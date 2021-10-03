﻿module Generator.Tests.CommandDefMappingTests

open Xunit
open FsUnit.Xunit
open FsUnit.CustomMatchers
open Generator.ArchetypeMapping
open Generator.RoslynUtils
open Generator.GeneralUtils
open Generator.Models
open Generator.Tests.UtilForTests
open Generator.CommandDefMapping
open Microsoft.CodeAnalysis
open Generator.Tests.TestData

// KAD: If the parens are ommitted here, FsUnit gives an error about only one constructor allowed.
//      Maybe it is an error that FS shoudl have captured.
type ``When evaluating handlers``() =

    [<Fact>]
    member _.``Parameters retrieved from Handler``() =
        let (archetypes, model) = archetypesAndModelFromSource OneMapping.MapInferredStatements
        let expected = [("one", "string")]

        let parameters = ParametersFromArchetype archetypes[0] model
        let actual = 
            [ for tuple in parameters do
                match tuple with 
                | (name, t) -> (name, t.ToString())]

        actual |> should matchList expected


type ``When building CommandDef parts``() =

    
    [<Fact>]
    member _.``Argument is found``() =
        let parameters = [("two", "int")]
        let raw = [ ArgArchetype { Id = "two"; Name = "two"; Aliases = []; HiddenAliases = [] } ]
        let expected = Some {
            ArgId = "two" 
            Name = "two"
            Description = None
            Required = None
            TypeName = "int" }

        let (arg, options) = argAndOptions parameters raw

        options |> should be Empty
        arg |> should be (ofCase <@ Some @>)
        arg |> should equal expected 

    
    [<Fact>]
    member _.``One option is found``() =
        let parameters = [("one", "string")]
        let raw = [ OptionArchetype { Id = "one"; Name = "one"; Aliases = []; HiddenAliases = [] } ]
        let expected = [{
            OptionId = "one" 
            Name = "one"
            Description = None
            Aliases = []
            Required = None
            TypeName = "string" }]

        let (arg, options) = argAndOptions parameters raw

        // KAD: This is related to the problem argAndOptions. This should be None, not null
        arg |> should be (ofCase <@ None @>)
        //arg |> should be null
        options |> should equal expected

    [<Fact>]
    member _.``Two options and one argument are found``() =
        let parameters = [("one", "string"); ("two", "int"); ("three", "int")]
        let raw = 
            [ OptionArchetype { Id = "one"; Name = "one"; Aliases = []; HiddenAliases = [] } 
              ArgArchetype { Id = "two"; Name = "two"; Aliases = []; HiddenAliases = [] } 
              OptionArchetype { Id = "three"; Name = "three"; Aliases = []; HiddenAliases = [] } ]

        let optionDefOne = {
            OptionId = "one" 
            Name = "one"
            Description = None
            Aliases = []
            Required = None
            TypeName = "string" };
        let optionDefThree = {
            OptionId = "three" 
            Name = "three"
            Description = None
            Aliases = []
            Required = None
            TypeName = "int" }
        let expectedOptions = [ optionDefOne; optionDefThree ]
        let expectedArg = Some {
            ArgId = "two" 
            Name = "two"
            Description = None
            Required = None
            TypeName = "int" }

        let (arg, options) = argAndOptions parameters raw

        arg |> should equal expectedArg
        options |> should equal expectedOptions


    [<Fact>]
    member _.``CommandDef is built``() =
        let source = AddMapStatements false ThreeMappings.MapInferredStatements
        let mutable model = null
        let result = 
            InvocationsAndModelFrom source
            |> Result.map (
                fun (invocations, m) -> 
                    model <- m
                    invocations)
            |> Result.bind ArchetypeInfoListFrom
            |> Result.map ArchetypeInfoTreeFrom
            |> Result.map (CommandDefFrom model)

        let actual = 
            match result with 
            | Ok tree -> tree
            | Error err -> invalidOp $"Failed to build tree {err}" // TODO: Work on error reporting

        result |> should equal (Ok ThreeMappings.CommandDef)
