﻿module DslStructuralTests


open Xunit
open Generator.Language
open Common
open DslKeywords
open DslCodeBuilder
open type Generator.Language.LanguageHelpers

let NameFromSimpleName (namedItem: NamedItem) =
    match namedItem with
    | SimpleNamedItem n -> n
    | _ -> invalidOp "Simple name not found"
  
let NameAndGenericsFromName (namedItem: NamedItem) =
    match namedItem with
    | GenericNamedItem (n, g) -> 
        n, 
        [for x in g do 
            match x with 
            | SimpleNamedItem gn -> gn
            | _ -> invalidOp "Generic was not a simple name" ]
    | _ -> invalidOp "Generic name not found"

type ``When creating a namespace``() =
    let namespaces: Namespace list = []

    // KAD-Don-Chet: It appears that we don't support the empty case.
    [<Fact(Skip="Can't create an empty thing yet. May drop requirement.")>]
    member _.``Can create a namespace``() =
        let nspace = "George"
        let expected = { NamespaceModel.NamespaceName = nspace; Usings = []; Classes = [] }

        // KAD-Don-Chet: I thought when I created a Zero method, I'd be able to remove the body of these expressions if they were empty.
        let actual = 
             Namespace(nspace) {
                Using ""
                }
        Assert.Equal(expected, actual)


    [<Fact>]
    member _.``Can add using to namespace``() =
        let nspace = "George"
        let usingName = "Fred"
        let expected = 
            { NamespaceModel.NamespaceName = nspace
              Usings = [ { UsingModel.UsingNamespace = usingName; Alias = None }]
              Classes = [] }
        let actual = 
             Namespace(nspace) {
                Using usingName
                }

        Assert.Equal(expected, actual)
 

    [<Fact>]
    member _.``Can add using with alias to namespace``() =
        let nspace = "George"
        let usingName = "Fred"
        let alias = "F"
        let expected = 
            { NamespaceModel.NamespaceName = nspace
              Usings = [ { UsingModel.UsingNamespace = usingName; Alias = Some alias }]
              Classes = [] }
        let actual = 
             Namespace(nspace) {
                Using (usingName, Alias, alias)
                }

        Assert.Equal(expected, actual)


    [<Fact>]
    member _.``Can add multiple usings to namespace``() =
        let nspace = "George"
        let usingName0 = "Fred"
        let usingName1 = "Sally"
        let usingAlias1 = "S"
        let usingName2 = "Sue"
        let expected = 
            { NamespaceModel.NamespaceName = nspace
              Usings = [ 
                { UsingModel.UsingNamespace = usingName0; Alias = None }
                { UsingModel.UsingNamespace = usingName1; Alias = Some usingAlias1 }
                { UsingModel.UsingNamespace = usingName2; Alias = None }]
              Classes = [] }

        let actual = 
            Namespace(nspace) 
                {
                    Using usingName0
                    Using (usingName1, alias = usingAlias1)
                    Using usingName2
                }

        Assert.Equal(expected, actual)

        
    [<Fact>]
    member _.``Can add class to namespace``() =
        let nspace = "George"
        let className = "Fred"
        let expected = 
            { NamespaceModel.NamespaceName = nspace
              Usings = []
              Classes = [
                { ClassName = className
                  Scope = Public
                  Modifiers = []
                  InheritedFrom = NoBase
                  ImplementedInterfaces = []
                  Members = [] }] }

        let actual = 
            Namespace(nspace) {
                let x = 42
                Class(className) {
                        Public }
                }

        Assert.Equal(expected, actual)

        
    [<Fact>]
    member _.``Can add class and using to namespace``() =
        let nspace = "George"
        let className = "Fred"
        let usingName = "Bill"
        let expected = 
            { NamespaceModel.NamespaceName = nspace
              Usings = [{ UsingModel.UsingNamespace = usingName; Alias = None }]
              Classes = [
                { ClassName = className
                  Scope = Public
                  Modifiers = []
                  InheritedFrom = NoBase
                  ImplementedInterfaces = []
                  Members = [] }] }

        let actual = 
            Namespace(nspace) {
                Class(className) {
                    Public }
                Using usingName

                }

        Assert.Equal(expected, actual)


type ``When creating a class``() =
    
    [<Fact>]
    member _.``Can create class``() =
        let className = "George"
        let expected =
            { ClassName = className
              Scope = Public
              Modifiers = []
              InheritedFrom = NoBase
              ImplementedInterfaces = []
              Members = [] }
        let actual = 
            Class(className) { 
                Public
                }

        Assert.Equal(expected, actual)


    [<Fact>]
    member _.``Can create class with multiple modifiers``() =
        let className = "George"
        let expected =
            { ClassName = className
              Scope = Public
              Modifiers = [Modifier.Static; Async; Partial]
              InheritedFrom = NoBase
              ImplementedInterfaces = []
              Members = [] }
        let actual = Class(className) { 
                Public Modifier.Static Async Partial
                }

        Assert.Equal(expected, actual)


    // using inline data hacks because VS is terrible at displaying other theory approaches in VS 2019
    [<Theory>]
    [<InlineData(0)>]
    [<InlineData(1)>]
    [<InlineData(2)>]
    [<InlineData(3)>]
    [<InlineData(4)>]
    [<InlineData(5)>]
    [<InlineData(6)>]
    [<InlineData(7)>]
    [<InlineData(8)>]
    [<InlineData(9)>]
    member _.``Can create different scopes and modifiers``(pos: int) =
        let tupleFromCodeModel (codeModel: ClassModel) = 
            codeModel.Scope, codeModel.Modifiers
        let data = 
            [ (Public, []), Class("A") { Public }
              (Public, [Modifier.Async]), Class("A") { Public Async }
              (Public, [Modifier.Abstract]), Class("A") { Public Abstract }
              (Public, [Modifier.Static; Abstract; Partial]), Class("A") { Public Modifier.Static Abstract Partial}
              (Public, [Modifier.Abstract; Async; Partial; Sealed]), Class("A") { Public Abstract Async Partial Sealed}
              (Private, []), Class("A") { Private }
              (Internal, [Modifier.Async]), Class("A") { Internal Async }
              (Protected, [Modifier.Abstract]), Class("A") { Protected Abstract }
              (Private, [Modifier.Static; Abstract; Partial]), Class("A") { Private Modifier.Static Abstract Partial}
              (Protected, [Modifier.Abstract; Async; Partial; Sealed]), Class("A") { Protected Abstract Async Partial Sealed}
            ]

        match data[pos] with 
        | expected, actual -> Assert.Equal(expected, tupleFromCodeModel actual)
  
  
    [<Fact>]
    member _.``Can create class with explicit generic types``() =
        let className = "George"
        let genericNames = ["string"; "int"]
        let genericNamedItems = 
            [ for n in genericNames do NamedItem.Create n [] ]
        let expected =
            { ClassName = NamedItem.Create className genericNamedItems
              Scope = Unknown
              Modifiers = []
              InheritedFrom = NoBase
              ImplementedInterfaces = []
              Members = [] }
        let actual = Class(className) {
                Generics [ SimpleNamedItem genericNames[0]; SimpleNamedItem genericNames[1] ]
                }

        Assert.Equal(expected, actual)

    [<Fact>]
    member _.``Can create class with stringified generic types``() =
        let className = "George"
        let genericNames = ["string"; "int"]
        let genericName = $"{className}<{genericNames[0]}, {genericNames[1]}>"
        let genericNamedItems = 
            [ for n in genericNames do NamedItem.Create n [] ]
        let expected =
            { ClassName = NamedItem.Create className genericNamedItems
              Scope = Unknown
              Modifiers = []
              InheritedFrom = NoBase
              ImplementedInterfaces = []
              Members = [] }
        let actual = Class(className) {
                Generics [ SimpleNamedItem genericNames[0]; SimpleNamedItem genericNames[1] ]
                }

        Assert.Equal(expected, actual)


    [<Fact>]
    member _.``Can create class with base class``() =
        let className = "George"
        let genericName = "Bart"
        let expected =
            { ClassName = className
              Scope = Unknown
              Modifiers = []
              InheritedFrom = SomeBase genericName
              ImplementedInterfaces = []
              Members = [] }
        let actual = 
            Class(className) {
                InheritedFrom (SimpleNamedItem genericName)
                }

        Assert.Equal(expected, actual)


    [<Fact>]
    member _.``Can create class with implemented interfaces``() =
        let className = "George"
        let interfaceNames = ["A"; "B"]
        let expectedInterfaces = 
            [ for n in interfaceNames do ImplementedInterface (NamedItem.Create n []) ]
        let expected =
            { ClassName = className
              Scope = Unknown
              Modifiers = []
              InheritedFrom = NoBase
              ImplementedInterfaces = expectedInterfaces
              Members = [] }
        let actual = 
            Class(className) {
                ImplementedInterfaces [| for i in interfaceNames do ImplementedInterface (NamedItem.Create i [])|]
                }

        Assert.Equal(expected, actual)

    
    [<Fact>]
    member _.``Can add field to class``() =
        let className = "George"
        let fieldName = "A"
        let fieldType = SimpleNamedItem "int"
        let expectedField = { FieldName = fieldName
                              FieldType = fieldType
                              Modifiers = []
                              Scope = Private
                              InitialValue = None}
        let expected =
            { ClassName = className
              Scope = Public
              Modifiers = []
              InheritedFrom = NoBase
              ImplementedInterfaces = []
              Members = [ expectedField ] }
        let actual = 
            Class(className) {
                Public  
                Members [
                    Field(fieldName, fieldType) {
                        Private
                        }]
                }
        //let codeModelDesired =
        //    Class(className) {
        //        Public  // KAD-Chet-Don: Uncommenting this shows current issue
        //        Field(fieldName, fieldType) {
        //            Public
        //            }
        //        }

        Assert.Equal(expected, actual)


type ``When creating a field``() =

    [<Fact>]
    member _.``Can create a field``() =
        let name = "A"
        let fieldType = SimpleNamedItem "int"
        let codeModel = 
            Field(name, fieldType) {
                Public
                }

        Assert.Equal(name, codeModel.FieldName)
        Assert.Equal(fieldType, codeModel.FieldType)
  
     
type ``When creating a property``() =

    [<Fact>]
    member _.``Can create a property``() =
        let name = "A"
        let propertyType = SimpleNamedItem "int"
        let codeModel = 
            Property(name, propertyType) {
                Public
                }

        Assert.Equal(name, codeModel.PropertyName)
        Assert.Equal(propertyType, codeModel.Type)


type ``When creating a constructor``() =

    [<Fact>]
    member _.``Can create a ctor``() =
        let expected = { ConstructorModel.Scope = Public; Modifiers = []; Parameters = []; Statements = [] }
        let actual = 
            Constructor() {
                Public
                }

        Assert.Equal(expected, actual)

        
type ``When creating a method``() =
        
    [<Fact>]
    member _.``Can create a method``() =
        let name = "A"
        let returnType = Void
        let codeModel = 
            Method(SimpleNamedItem name, returnType) {
                Public
                }
        
        Assert.Equal(name, NameFromSimpleName codeModel.MethodName)
        Assert.Equal(returnType, codeModel.ReturnType)
        
