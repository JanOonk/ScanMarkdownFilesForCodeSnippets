# Scan Markdown Files For CodeBlocks
Scans Markdown files for inline and fenced codeblocks and shows stats about codeblocks usage and what syntax (json, console, sh, javascript, md, ...) was used.
It's extremely fast and uses the CommonMark markdown compliant MarkDig library.

For syntax on how to make (fenced) codeblocks and use syntax highlighting see https://www.markdownguide.org/extended-syntax/#fenced-code-blocks and https://www.markdownguide.org/extended-syntax/#syntax-highlighting

## How to use
```console
ScanMarkdownFiles v1.3 - 06 Feb 2022
Syntax: <program> <rootFolderToScan> [filePattern = *.md] [-onlyTopLevel = no] [-hideFencedCodeBlocks = no] [-hideInlineCodeBlocks = no]
  For example:
    ScanMarkDownFiles /sources/myrepo
      will scan all /sources/myrepo including subdirs for *.md files and show the contents of both inline and fenced code blocks

    ScanMarkDownFiles /sources/myrepo -hideFencedCodeBlocks -hideInlineCodeBlocks
      will scan all /sources/myrepo including subdirs for *.md files

    ScanMarkDownFiles /sources/myrepo *.mdwn
      will scan all /sources/myrepo including subdirs for *.mdwn files

    ScanMarkDownFiles /sources/myrepo *.md -onlyTopLevel
      will only scan the top level directory /sources/myrepo for *.md files
```

Example output:
```console
Found 852 files matching '*.md' in 'c:\temp\iota-repos':

\integration-services-master\CHANGELOG.md
\integration-services-master\MIGRATION.md
  1 inline code block(s) found
    Inline code block on line 22 with contents: ts-node src/tools/migration/migration-to-0.1.3.ts
\integration-services-master\README.md
\iota.c-dev\CHANGELOG.md
\iota.c-dev\README.md
  5 fenced code block(s) found:
    Fenced code block on lines 56-58 with type 'shell'
      Contents (1 line):
        sudo apt install build-essential libcurl4-openssl-dev pkg-config
    Fenced code block on lines 66-72 with type 'shell'
      Contents (5 lines):
        git clone https://github.com/iotaledger/iota.c.git
        cd iota.c
        mkdir build && cd build
        cmake -G Ninja -DCMAKE_C_COMPILER=clang-11 -DCMAKE_CXX_COMPILER=clang++-11 -DIOTA_WALLET_ENABLE:BOOL=TRUE -DCMAKE_INSTALL_PREFIX=$PWD ..
        ninja && ninja test

[ ... ]

Overall stats:
    10 inline code block types found in 6 files
  1555 fenced code block types found in 270 files:
      28 unique fenced code block types found:
       405x found in  92 file(s): <no type>
       133x found in  47 file(s): bash
        10x found in   2 file(s): c
        68x found in  14 file(s): console
         2x found in   1 file(s): csv
         1x found in   1 file(s): diff
       155x found in  45 file(s): go
         8x found in   2 file(s): java
        22x found in   9 file(s): javascript
        60x found in  21 file(s): js
       149x found in  43 file(s): json
        21x found in   9 file(s): log
         3x found in   3 file(s): md
         2x found in   2 file(s): mdx-code-block
         3x found in   1 file(s): no_run
         3x found in   3 file(s): plaintext
       117x found in   6 file(s): python
         7x found in   1 file(s): rs
        67x found in  17 file(s): rust
        15x found in   4 file(s): sh
       249x found in  47 file(s): shell
         2x found in   2 file(s): solidity
        13x found in   3 file(s): toml
         3x found in   2 file(s): vbnet
        31x found in  24 file(s): yaml
         2x found in   1 file(s): yml
```
