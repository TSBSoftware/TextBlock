# TextBlock

Makes F# multiline strings work like C# Raw String Literals, or Java Text Blocks.

This project was inspired by an [article](https://www.bytesize.press/java-text-blocks) related to Java Text Blocks. This functionality felt like a missing feature in the F# language, and I wanted to make it available to everyone.


## Who is it for?

F# developers who would like to utilize multiline strings to format content, such as: html, xml, sql, markdown, templates, etc.


## Features

TextBlock provides a robust set of tools to simplify working with multiline strings in F#:

  - **Automatic Indentation Stripping**: Removes unwanted leading whitespace (spaces or tabs) from each line, based on the minimum indentation, ensuring clean output for formats like HTML, SQL, or Markdown.
  - **Newline Escaping**: Supports backslash (`\`) at the end of a line to suppress newlines, allowing long content to be split across lines in source code without affecting the output.
  - **Trailing Space Preservation**: Preserves trailing spaces using a pipe (`|`) marker at the end of a line, ideal for maintaining intentional spacing in templates or formatted text.
  - **Custom Indentation**: Allows additional indentation with a user-defined character (default is a space) and count, enabling flexible formatting for nested or styled output.
  - **Embedded Newline Support**: Correctly handles embedded newline characters (e.g., via `Environment.NewLine`), ensuring accurate rendering of dynamic content like multi-line SQL queries or email templates.
  - **Cross-Platform Compatibility**: Uses `Environment.NewLine` for line endings, ensuring consistent behavior across Windows (`\r\n`) and other platforms (`\n`).


## Why Use TextBlock?

| Feature                  | F# Multiline Strings | TextBlock | C# Raw String Literals | Java Text Blocks |
|--------------------------|----------------------|-----------|------------------------|------------------|
| Auto-indent stripping    | No                   | Yes       | Yes                    | Yes              |
| Newline escaping         | No                   | Yes       | Yes                    | Yes              |
| Trailing space control   | No                   | Yes       | Partial                | Yes              |


## Getting Started

To start using TextBlock, install the NuGet package into your project file.

```bash
dotnet add package TSBSoftware.TextBlock
```

Open the TextBlock namespace to use the extension method.

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

This will produce the following string:

```plaintext
<div>
    <p>Hello</p>
</div>
```

Without using TextBlock, there would be unwanted leading indentation because of how F# processes multiline strings:

```plaintext
    <div>
        <p>Hello</p>
    </div>
```


## Additional Examples

Strings can contain embedded newline characters.

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

```plaintext
This content has
several lines
in
the text.
```

Newlines can be escaped with a backslash.

```fsharp
let myText =
    """
    Hello \
    World!
    """
        .TextBlock()
```

This removes the newline character and combines the text into a single line:

```plaintext
Hello World!
```

Spaces at the end of each line can be preserved.

```fsharp
let blockedLines =
    """
    BlockLine   |
    Text        |
    """
        .TextBlock()
```

This will produce the following. Trailing spaces are shown as dots for clarity.

```plaintext
BlockLine...
Text........
```

You can apply additional indentation with a specified level and an optional character (default is a space).

```fsharp
let someHtml =
    """
    <div>
        <p>Hello</p>
    </div>
    """
        .TextBlock(indent = 4, indentChar = '.')
```

This will produce the following:

```plaintext
....<div>
....    <p>Hello</p>
....</div>
```

## Notes

  - **Repeated Application**: TextBlock is designed for one-shot processing of multiline strings. Applying it multiple times (e.g., `myText.TextBlock().TextBlock()`) is not recommended, as it may alter indentation or formatting unexpectedly.
 
  - **Platform Newlines**: Output uses `Environment.NewLine`, which may produce `\r\n` on Windows or `\n` on other platforms, ensuring compatibility with your target systems.


## License
   
   - **License**: MIT (see [LICENSE](https://github.com/TSBSoftware/TextBlock/blob/main/LICENSE) for details)


## Source Code

The source code and tests are available on [GitHub](https://github.com/TSBSoftware/TextBlock).