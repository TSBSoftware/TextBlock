# TextBlock

Makes F# multiline strings work like C# Raw string literal, or Java text blocks.

## Who is it for?

F# developers who would like to utilize multiline strings to format content, such as: html, xml, sql, templates, etc.

## Getting Started

To start using TextBlock, install the nuget package into your project file.

```
dotnet add package TextBlock
```

Then, open the namespace to expose the extension.

```fsharp
open TextBlock

let myText =
    """
    <div>
        <p>Hello</p>
    </div>
    """
        .TextBlock()
```

## Extra Examples

Strings can contain imbedded newline characters.

```fsharp
let embeddedNewlines =
    let nl = System.Environment.NewLine
    $"""
    This content has {nl}several lines
    in {nl}the text.
    """
        .TextBlock()
```

This will produce

```
This content has
several lines
in
the text.
```

Spaces at the end of each line can be preserved.

```fsharp
let blockedLines =
    """
    Blocked   |
    Text      |
    """
        .TextBlock()
```

This will produce the following. Spaces represended by spaces.

```
Blocked...
Text......
```

Additional indentation can be applied. Indentation character can optionally be applied,
which is a space by default. We will use a period to show what is applied.

```fsharp
let someHtml =
    """
    <div>
        <p>Hello</p>
    </div>
    """
        .TextBlock(indent = 4, indentChar = '.')
```

Will produce indented with the specified intentation.

```
....<div>
....    <p>Hello</p>
....</div>
```
