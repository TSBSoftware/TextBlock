﻿module TSBSoftware.TextblockTests

open System

open NUnit.Framework
open TextBlock

let nl = Environment.NewLine

[<Test>]
let ``Empty string gets returned.`` () =
    let actual =
        """
        """
            .TextBlock()

    let expected = ""
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``Simple value on one line is just a string.`` () =
    let actual = """Hello"""
    let expected = "Hello"
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``One line string gets returned with no padding.`` () =
    let actual =
        """
        Hello, World!
        """
            .TextBlock()

    let expected = "Hello, World!"
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``Two line string gets returned with no padding.`` () =
    let actual =
        """
        Hello, World!
        I am me.
        """
            .TextBlock()

    let expected = $"Hello, World!{nl}I am me."
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``Newline characters can be escaped.`` () =
    let actual =
        """
        Hello, World! \
        I am me. \
        Who are you?
        """
            .TextBlock()

    let expected = $"Hello, World! I am me. Who are you?"
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``Spaces that would be stripped can represented by \s.`` () =
    let actual =
        """
        Hello, World!\s
        Who are you?\s\s\s
        """
            .TextBlock()

    let expected = $"Hello, World! {nl}Who are you?   "
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``Indented content is preserved.`` () =
    let actual =
        """
        <div>
            <p>Hello</p>
        </div>
        """
            .TextBlock()

    let expected = $"<div>{nl}    <p>Hello</p>{nl}</div>"
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``Indented content is preserved when first line is indented deeper.`` () =
    let actual =
        """
            <p>Hello</p>
        </div>
        """
            .TextBlock()

    let expected = $"    <p>Hello</p>{nl}</div>"
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``All indented content is preserved when not lined up with quotes.`` () =
    let actual =
        """
                <p>Hello</p>
            </div>
        """
            .TextBlock()

    let expected = $"        <p>Hello</p>{nl}    </div>"
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``Lines ending in | keep trailing spaces.`` () =
    let actual =
        """
        This text must    |
        be blocked        |
        """
            .TextBlock()

    let expected = $"This text must    {nl}be blocked        "
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``User definded indent can be provided.`` () =
    let actual =
        """
        A
         B
          C
        """
            .TextBlock(indent = 4)

    let expected = $"    A{nl}     B{nl}      C"
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``User definded indent and indent char can be provided.`` () =
    let actual =
        """
        A
         B
          C
        """
            .TextBlock(indent = 4, indentChar = '+')

    let expected = $"++++A{nl}++++ B{nl}++++  C"
    Assert.That(actual, Is.EqualTo(expected))

[<Test>]
let ``All features are supported when mixed.`` () =
    let actual =
        """
          A
        B  |
        
        C\
         D\
          E
        """
            .TextBlock(indent = 1, indentChar = '+')

    let expected = $"+  A{nl}+B  {nl}+{nl}+C D  E"
    Assert.That(actual, Is.EqualTo(expected))
