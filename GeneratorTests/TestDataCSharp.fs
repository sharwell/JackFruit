﻿/// This module extends the basic code elements with the most common test cases
/// These are expected to be used with "with" syntax to add more specific local test cases
module Generator.Tests.TestData

open Generator.Language
open Common
open Generator.LanguageExpressions
open Generator.LanguageStatements
open Generator.LanguageHelpers


type TestData<'T> = { Data: 'T; CSharp: string list } // Add VB later

type TestDataBlock<'T> =
    { Data: 'T
      CSharpOpen: string list
      CSharpBlock: string list
      CSharpClose: string list }

// Where a class may be used, use NamedType, even if it will generally be an instance
type NamedItem with
    static member ForTesting =
        let data = NamedItem.Create("RonWeasley", [])
        { Data = data
          CSharp = [ "RonWeasley(JackRussell)" ] }

type InvocationModel with
    static member ForTesting =
        let data =
            { Instance = NamedItem.ForTesting.Data
              MethodName = SimpleNamedItem "JackRussell"
              ShouldAwait = false
              Arguments = [] }

        { Data = data
          CSharp = [ "RonWeasley(JackRussell)" ] }

type InstantiationModel with
    static member ForTesting =
        let data =
            { TypeName = NamedItem.ForTesting.Data
              Arguments = [] }

        { Data = data
          CSharp = [ "new RonWeasley()" ] }

type ComparisonModel with
    static member ForTesting =
        let data =
            { Left = SymbolLiteral (Symbol "left")
              Right = StringLiteral "qwerty"
              Operator = Operator.Equals }

        { Data = data
          CSharp = [ "left = \"querty\"" ] }

type IfModel with
    static member ForTesting =
        let data =
            { IfCondition =
                { Left = SymbolLiteral (Symbol "A")
                  Right = Literal "42"
                  Operator = Operator.Equals }
              Statements = [] }

        { Data = data
          CSharpOpen = [ "if (A == 42)"; "{" ]
          CSharpBlock = []
          CSharpClose = [ "}" ] }

type ForEachModel with
    static member ForTesting =
        let data =
            { LoopVar = "x"
              LoopOver = "listOfThings"
              Statements = [] }

        { Data = data
          CSharpOpen =
            [ "foreach (var x in listOfThings)"
              "{" ]
          CSharpBlock = []
          CSharpClose = [ "}" ] }

type AssignmentModel with
    static member ForTesting =
        let data =
            { Variable = "item"
              Value = StringLiteral "boo!" }

        { Data = data
          CSharp = [ "item = \"boo!\";" ] }


type AssignWithDeclareModel with
    static member ForTesting =
        let data =
            { Variable = "item"
              TypeName = None
              Value = StringLiteral "boo!" }

        { Data = data
          CSharp = [ "var item = \"boo!\";" ] }

type ParameterModel with
    static member ForTesting =
        let data =
            { ParameterName = "param1"
              Type = NamedItem.Create("string", [])
              Style = Normal }

        { Data = data
          CSharp = [ "string param1" ] }

type MethodModel with
    static member ForTesting =
        let data = MethodModel.Create ((SimpleNamedItem "MyMethod"), (ReturnType (NamedItem.Create("string", []))))

        { Data = data
          CSharpOpen = [ "public string MyMethod()"; "{" ]
          CSharpBlock = []
          CSharpClose = [ "}" ] }

type PropertyModel with
    static member ForTesting =
        let data =
            { PropertyName = "MyProperty"
              Type = NamedItem.Create("MyReturnType", [])
              Scope = Public
              Modifiers = []
              GetStatements = []
              SetStatements = [] }

        { Data = data
          CSharpOpen = [ "public MyReturnType MyProperty"; "{"]
          CSharpBlock = []
          CSharpClose = [ "}"] }

type ClassModel with
    static member ForTesting =
        let data =
            ClassModel.Create (NamedItem.ForTesting.Data, Public)
         

        { Data = data
          CSharpOpen = [ "public class RonWeasley"; "{" ]
          CSharpBlock = []
          CSharpClose = [ "}" ] }

type UsingModel with
    static member ForTesting =
        let data = { UsingNamespace = "System"; Alias = None }

        { Data = data
          CSharp = [ "using System;" ]}

type NamespaceModel with
    static member ForTesting =
        let data =
            { NamespaceName = "MyNamespace"
              Usings = []
              Classes = [] }

        { Data = data
          CSharpOpen = [ "namespace MyNamespace"; "{" ]
          CSharpBlock = []
          CSharpClose = [ "}" ] }
