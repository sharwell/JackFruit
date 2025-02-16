﻿module Generator.LanguageExpression

open Generator.Language
open Common

let x = 42

//let method 
//    (scope: Scope)
//    (returnType: ReturnType)
//    (name: NamedItem) 
//    (parameters: ParameterModel list)
//    (statements: IStatement list) =
//    Member.Method
//        { MethodName = name
//          Scope = scope
//          StaticOrInstance = Instance 
//          IsExtension = false
//          IsAsync = false
//          ReturnType = returnType
//          Parameters = parameters
//          Statements = statements
//        }


//let asyncMethod 
//    (scope: Scope)
//    (returnType: ReturnType)
//    (name: NamedItem) 
//    (parameters: ParameterModel list)
//    (statements: IStatement list) =
//    Member.Method
//        { MethodName = name
//          Scope = scope
//          StaticOrInstance = Instance 
//          IsExtension = false
//          IsAsync = true
//          ReturnType = returnType
//          Parameters = parameters
//          Statements = statements
//        }


//let staticMethod 
//    (scope: Scope)
//    (returnType: ReturnType)
//    (name: NamedItem) 
//    (parameters: ParameterModel list)
//    (statements: IStatement list) =
//    Member.Method
//        { MethodName = name
//          Scope = scope
//          StaticOrInstance = Static 
//          IsExtension = false
//          IsAsync = false
//          ReturnType = returnType
//          Parameters = parameters
//          Statements = statements
//        }

//let ctor 
//    (scope: Scope)
//    (className: string)
//    (parameters: ParameterModel list)
//    (statements: IStatement list) =
//    Member.Constructor
//        { ClassName = className
//          Scope = scope
//          StaticOrInstance = Instance 
//          Parameters = parameters
//          Statements = statements
//        }

//let staticCtor
//    (scope: Scope)
//    (className: string)
//    (parameters: ParameterModel list)
//    (statements: IStatement list) =
//    Member.Constructor
//        { ClassName = className
//          Scope = scope
//          StaticOrInstance = Static 
//          Parameters = parameters
//          Statements = statements
//        }
         
//let field
//    (fieldType: NamedItem)
//    (name: string)
//    (initialValue: IExpression) =
//    Member.Field
//        { FieldName = name
//          StaticOrInstance = Instance 
//          IsReadonly = false
//          FieldType = fieldType
//          Scope = Private
//          InitialValue = Some initialValue
//        }
        
//let readonlyField 
//    (fieldType: NamedItem)
//    (name: string) =
//    Member.Field
//        { FieldName = name
//          StaticOrInstance = Instance 
//          IsReadonly = true
//          FieldType = fieldType
//          Scope = Private
//          InitialValue = None
//        }


//let extensionMethod 
//    (scope: Scope) 
//    (returnType: ReturnType)
//    (name: string) 
//    (parameters: ParameterModel list)
//    (statements: IStatement list) =
//    Member.Method
//        { MethodName = SimpleNamedItem name
//          Scope = scope
//          StaticOrInstance = Static
//          IsExtension = true
//          IsAsync = false
//          ReturnType = returnType
//          Parameters = parameters
//          Statements = statements
//        }

////let clsWithInterfaces
////    (scope: Scope) 
////    (name: string) 
////    (interfaces: NamedItem list)
////    (members: Member list) =
////    Member.Class
////        { ClassName = SimpleNamedItem name
////          Scope = scope
////          StaticOrInstance = Instance 
////          IsAsync = false
////          IsPartial = false
////          InheritedFrom = None
////          ImplementedInterfaces = interfaces
////          Members = members
////        }


//let param
//    (name: string)
//    (paramType: NamedItem) =
//    { ParameterName = name
//      Type = paramType
//      Default = None
//      IsParams = false}

////let assign 
////    (item: string)
////    (value: ExpressionModel) =
////    Statement.Assign 
////        { Item = item
////          Value = value }

//let prop
//    (scope: Scope)
//    (propertyType: NamedItem)
//    (propertyName: string)
//    (getStatements: IStatement list)
//    (setStatements: IStatement list) =
//    Member.Property
//        { PropertyName = propertyName
//          Type = propertyType
//          StaticOrInstance = Instance
//          Scope = scope
//          GetStatements = getStatements
//          SetStatements = setStatements }

//let ifThen 
//    (condition: ExpressionModel)
//    (ifStatements: IStatement list) =
//    Statement.If
//        { Condition = condition
//          Statements = ifStatements
//          Elses = []}

//let invoke 
//     ( instance: string)
//     ( methodName: string)
//     ( arguments: IExpressionModel list) =
//     ExpressionModel.Invocation 
//        { Instance = SimpleNamedItem instance 
//          MethodName = SimpleNamedItem methodName
//          ShouldAwait = false
//          Arguments = arguments }

//let invokeAwait
//    ( instance: string)
//    ( methodName: string)
//    ( arguments: ExpressionModel list) =
//    ExpressionModel.Invocation 
//       { Instance = SimpleNamedItem instance 
//         MethodName = SimpleNamedItem methodName
//         ShouldAwait = true
//         Arguments = arguments }

    
    