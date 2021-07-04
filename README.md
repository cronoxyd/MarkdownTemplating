# MarkdownTemplating
This is a proof-of-concept application that turns a markdown-based template text with embedded javascript expressions into a HTML document.

## Installation
Currently, no releases are available. You will have to clone or download the source code, build it and execute the binary.

## Usage
Input a markdown text into the left-hand textfield and it will be converted and previewed in the right-hand web browser.

You can embed a javascript expression into the Markdown text by wrapping it in double curly-braces, for example a footer with an automatically updating year for the copyright notice:

```md
&copy; {{new Date().getFullYear()}} cronoxyd
```

## References / credits
* [ClearScript](https://github.com/microsoft/ClearScript)
* [Markdig](https://github.com/xoofx/markdig)