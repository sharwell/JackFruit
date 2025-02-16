﻿module rec Generator.CSharpLanguageExtensions

//open Generator.Language
//open System
//open Common
//open System.Runtime.CompilerServices

//type Operator with
//    member this.Output =
//        match this with 
//        | Equals -> "=="
//        | NotEquals -> "!="
//        | GreaterThan -> ">"
//        | LessThan -> "<"
//        | GreaterThanOrEqualTo -> ">="
//        | LessThanOrEqualTo -> "<="

//type NamedItem with
//    member this.Output =
//        match this with 
//        | SimpleNamedItem name -> name
//        | GenericNamedItem (name, genericTypes) ->
//            let generics = 
//                match genericTypes with 
//                | [] -> ""
//                | _ -> 
//                    let x = [ for t in genericTypes do t.Output ]
//                    let y = String.Join(", ", x)
//                    $"<{y}>"
//            $"{name}{generics}"


//type Invocation with 
//    member this.Output = 
//        $"{this.Instance.Output}.{this.MethodName.Output}({OutputArguments this.Arguments})" 

//type Comparison with 
//    member this.Output = 
//        $"{this.Left.Output} {this.Operator.Output} {this.Right.Output}"

//type Instantiation with 
//    member this.Output = 
//        $"new {this.TypeName.Output}({OutputArguments this.Arguments})"  // TODO: Arguments

//type Expression with 
//    member this.Output =
//        match this with 
//        | Invocation x -> x.Output
//        | Comparison x -> x.Output
//        | Instantiation x -> x.Output
//        | StringLiteral x -> "\"" + x + "\""
//        | NonStringLiteral x -> x
//        | Symbol x -> x
//        | Comment x -> $"// {x}"
//        | Pragma x -> $"# {x}"
//        | Null _ -> "null"

//type Assignment with 
//     member this.Output =
//        $"{this.Item} = {this.Value}"


//let OutputParameters (parameters: Parameter list) = 
//    let getDefault parameter =
//        match parameter.Default with 
//            | None -> ""
//            | Some def -> " " + def.Output
    
//    let s = [ for param in parameters do
//                $"{param.Type.Output} {param.ParameterName}{getDefault param}"]
//    String.Join(", ", s)
    
//let OutputArguments (arguments: Expression list) = 
//    let s = [ for arg in arguments do
//                $"{arg.Output}"]
//    String.Join(", ", s)

