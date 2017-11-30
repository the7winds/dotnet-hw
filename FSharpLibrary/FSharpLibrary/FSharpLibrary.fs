namespace FSharpLibrary

module Lib =

    // evaluate n-th fibonacci number
    let rec _fibonacci n p1 p2 = 
        if n = 0 then p1 else _fibonacci (n-1) p2 (p1 + p2)

    let fibonacci n = _fibonacci n 1 1

    // revert list
    let rec _revert l r =
        match l with
        | [] -> r
        | x::xs -> _revert xs (x::r)

    let revert l = _revert l []

    // merge-sort
    let rec merge l r =
        match (l, r) with
        | ([], r) -> r
        | (l, []) -> l
        | (x::xs, y::ys) -> if (x < y) then x::(merge xs r) else y::(merge l ys)

    let rec mergesort l = 
        match l with
        | [] -> []
        | x::[] -> l
        | _ -> 
            let (left, right) = List.splitAt (List.length l / 2) l in
            let ls = mergesort left in
            let rs = mergesort right in
                merge ls rs

    // prime sequence
    let rec testPrime n i =
        if (i * i > n) then true
        else if (n % i = 0) then false else testPrime n (i + 1)

    let rec firstPrimeAfter n = if testPrime n 2 then n else firstPrimeAfter (n + 1)

    let rec nthPrime n =
        match n with
        | 1 -> 2
        | n -> firstPrimeAfter (nthPrime (n-1) + 1)

    let prime = Seq.initInfinite nthPrime

    // evaluate expression
    type Expr = 
        | Value of int
        | Add of Expr * Expr
        | Sub of Expr * Expr
        | Mul of Expr * Expr
        | Div of Expr * Expr
        | Mod of Expr * Expr

    let rec eval e =
        match e with
        | Value v -> v
        | Add (x, y) -> eval x + eval y
        | Sub (x, y) -> eval x - eval y
        | Mul (x, y) -> eval x * eval y
        | Div (x, y) -> eval x / eval y
        | Mod (x, y) -> eval x % eval y
