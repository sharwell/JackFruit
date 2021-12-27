﻿module rec Generator.Language

open Generator
open Common
open DslKeywords
open System

type Modifier =
    | Static
    | Async
    | Extension
    | Partial
    | Abstract
    | Sealed
    | Readonly
    static member Contains modifier list =
        List.exists (fun x -> x = modifier) list

type IMember = interface end
type IStatement = interface end
type IExpression = interface end
type ICompareExpression = 
    interface 
    inherit IExpression
    end
type IStatementContainer<'T> = 
    abstract member AddStatements: IStatement list -> 'T

type IMethodLike<'T when 'T:> IMethodLike<'T>> =
    inherit IStatementContainer<'T>
    abstract member AddScopeAndModifiers: Scope -> Modifier list -> 'T

type ParameterModel =
    { ParameterName: string
      Type: NamedItem
      Default: IExpression option
      IsParams: bool}
    static member Create name paramType =
        { ParameterName = name
          Type = paramType
          Default = None
          IsParams = false }


type MethodModel =
    { MethodName: NamedItem
      ReturnType: ReturnType
      Scope: Scope
      Modifiers: Modifier list
      Parameters: ParameterModel list
      Statements: IStatement list}
    static member Create methodName returnType =
        { MethodName = methodName
          ReturnType = returnType
          Scope = Public
          Modifiers = []
          Parameters = []
          Statements = [] }
    interface IMember
    member this.AddStatements statements =
        { this with Statements = statements }
    member this.AddScopeAndModifiers scope modifiers = 
        { this with Scope = scope; Modifiers = modifiers }
    interface IMethodLike<MethodModel> with
        member this.AddStatements statements = this.AddStatements statements
        member this.AddScopeAndModifiers scope modifiers = this.AddScopeAndModifiers scope modifiers
     
 
type ConstructorModel =
    { Scope: Scope
      Modifiers: Modifier list
      Parameters: ParameterModel list
      Statements: IStatement list}
    static member Create className =
        { Scope = Public
          Modifiers = []
          Parameters = []
          Statements = [] }
    interface IMember
    member this.AddStatements statements =
        { this with Statements = statements }
    member this.AddScopeAndModifiers scope modifiers = 
        { this with Scope = scope; Modifiers = modifiers }
    interface IMethodLike<ConstructorModel> with
        member this.AddStatements statements = this.AddStatements statements
        member this.AddScopeAndModifiers scope modifiers = this.AddScopeAndModifiers scope modifiers

type PropertyModel =
    { PropertyName: string
      Type: NamedItem
      Scope: Scope
      Modifiers: Modifier list
      GetStatements: IStatement list
      SetStatements: IStatement list}
    static member Create propertyName propertyType =
        { PropertyName = propertyName
          Type = propertyType
          Scope = Public
          Modifiers = []
          GetStatements = []
          SetStatements = [] }
    interface IMember


type FieldModel =
    { FieldName: string
      FieldType: NamedItem
      Scope: Scope
      Modifiers: Modifier list
      InitialValue: IExpression option}
    static member Create fieldName fieldType =
        { FieldName = fieldName
          FieldType = fieldType
          Scope = Private
          Modifiers = []
          InitialValue = None }
    interface IMember


type InheritedFrom =
    | SomeBase of BaseClass: NamedItem
    | NoBase

type ImplementedInterface =
    | ImplementedInterface of Name: NamedItem
        
 
type ClassModel = 
    { ClassName: NamedItem
      Scope: Scope
      Modifiers: Modifier list
      InheritedFrom: InheritedFrom
      ImplementedInterfaces: ImplementedInterface list
      Members: IMember list}
    static member Create(className, scope) =
        { ClassName = className
          Scope = scope
          Modifiers = []
          InheritedFrom = NoBase
          ImplementedInterfaces = []
          Members = [] }
    static member Create(className) =
        ClassModel.Create((SimpleNamedItem className), Unknown)
    interface IMember


type UsingModel = 
    { UsingNamespace: string
      Alias: string option }
    static member Create nspace =
        { UsingNamespace = nspace
          Alias = None }


type NamespaceModel = 
    { NamespaceName: string
      Usings: UsingModel list
      Classes: ClassModel list}
    static member Create(name: string) =
        { NamespaceName = name
          Usings = []
          Classes = [] }    
    member this.AddUsings (usings: UsingModel list) =
        // Ignore usings with empty strings. They would be an error later
        let newUsings =
            [ for u in usings do
                if not (String.IsNullOrWhiteSpace u.UsingNamespace) then u ]
        { this with Usings = List.append this.Usings newUsings }
    member this.AddClasses (classes: ClassModel list) =
        { this with Classes = List.append this.Classes classes }

type ScopeAndModifiers =
    { Scope: Scope
      Modifiers: Modifier list }

type LanguageHelpers =
    static member Using (usingName: string) =
        UsingModel.Create usingName
    static member Using (usingName: string, ?w:AliasWord, ?alias: string) =
        match alias with 
        | None -> UsingModel.Create usingName
        | Some a ->
            if String.IsNullOrEmpty a then
                UsingModel.Create usingName
            else
                { UsingNamespace = usingName; Alias = Some a }
    //static member Public ([<ParamArray>] modifiers: Modifier[]) =
    //    { Scope = Scope.Public; Modifiers = List.ofArray modifiers }
    //static member Private ([<ParamArray>] modifiers: Modifier[]) =
    //    { Scope = Scope.Public; Modifiers = List.ofArray modifiers }
    //static member Internal ([<ParamArray>] modifiers: Modifier[]) =
    //    { Scope = Scope.Public; Modifiers = List.ofArray modifiers }
    //static member Protected ([<ParamArray>] modifiers: Modifier[]) =
    //    { Scope = Scope.Public; Modifiers = List.ofArray modifiers }
