# Scan Markdown Files For CodeBlocks
Scans Markdown files for (single/multi-line) codeblocks and shows stats about codeblocks usage and what syntax (json, console, sh, javascript, md, ...) was used.

For syntax on how to make (fenced) codeblocks and use syntax highlighting see https://www.markdownguide.org/extended-syntax/#fenced-code-blocks and https://www.markdownguide.org/extended-syntax/#syntax-highlighting

## How to use
```console
Syntax: <program> <rootFolderToScan> [filePattern = *.md] [-onlyTopLevel = no] [-hideMultiLineCodeBlocks = no] [-hideSingleLineCodeBlocks = no]
  For example:
    ScanMarkDownFiles /sources/myrepo
      will scan all /sources/myrepo including subdirs for *.md files and show the contents of both single and multi-line code blocks

    ScanMarkDownFiles /sources/myrepo -hideMultiLineCodeBlocks -hideSingleLineCodeBlocks
      will scan all /sources/myrepo including subdirs for *.md files

    ScanMarkDownFiles /sources/myrepo *.mdwn
      will scan all /sources/myrepo including subdirs for *.mdwn files

    ScanMarkDownFiles /sources/myrepo *.md -onlyTopLevel
      will only scan the top level directory /sources/myrepo for *.md files
```

Example output:
```console
Found 24 files matching '*.md' in 'c:\temp\docs-develop':

\license.md
\readme.md
  4 multi-line code block(s) found:
    Multiline code block on lines 10-12 with type 'sh'
      Contents (1 line):
        git clone https://github.com/iotaledger/docs.git
    Multiline code block on lines 16-18 with type 'sh'
      Contents (1 line):
        npm install
    Multiline code block on lines 23-25 with type 'sh'
      Contents (1 line):
        npm run dev
    Multiline code block on lines 39-58 with type 'md'
      Contents (18 lines):
        import { withRouter } from 'next/router'
        import WithMDX from '../../../lib/with-mdx'

        import { TerminalInput } from "../../../components/text/terminal"

        export const page = {
        title: 'Example Page',
        date: '19 Feburary 2018',
        editUrl: 'pages/path/for-editing/on-github.mdx',
        }

        export default withRouter(props => WithMDX(props, page))

        # H1 Title

        This is the content written in Markdown.

        <TerminalInput># this is how we show the terminal input</TerminalInput>x
  2 unique multi-line code block type(s) found:
    Number of occurences per type:
              sh:   3x
              md:   1x
\pages\compass\api-reference\reference.md
\pages\compass\introduction\background.md
\pages\compass\introduction\overview.md
\pages\compass\introduction\usecases.md
\pages\compass\knowledge-base\architecture.md
\pages\compass\knowledge-base\components.md
\pages\compass\knowledge-base\contribution.md
\pages\compass\quick-start\security.md
\pages\compass\quick-start\simple-install.md
\pages\hub\api-reference\reference.md
\pages\hub\introduction\overview.md
\pages\hub\introduction\usecases.md
\pages\hub\knowledge-base\components.md
\pages\hub\knowledge-base\contribution.md
\pages\hub\knowledge-base\exchange-implementation.md
\pages\hub\knowledge-base\hub-architecture.md
\pages\hub\knowledge-base\README.md
\pages\hub\quick-start\create-user.md
\pages\hub\quick-start\security.md
\pages\hub\quick-start\signing-server.md
  20 single-line code block(s) found
    Single line code block on line 204  with contents: sudo apt install gcc-7
    Single line code block on line 208  with contents: sudo apt-get install pkg-config zip g++ zlib1g-dev unzip python
    Single line code block on line 212  with contents: wget https://github.com/bazelbuild/bazel/releases/download/0.18.0/bazel-0.18.0-installer-linux-x86_64.sh
    Single line code block on line 216  with contents: chmod +x bazel-0.18.0-installer-linux-x86_64.sh
    Single line code block on line 220  with contents: ./bazel-0.18.0-installer-linux-x86_64.sh --user
    Single line code block on line 224  with contents: sudo apt-get install python-pyparsing
    Single line code block on line 232  with contents: sudo apt-key adv --recv-keys --keyserver hkp://keyserver.ubuntu.com:80 0xF1656F24C74CD1D8
    Single line code block on line 236  with contents: sudo add-apt-repository 'deb [arch=amd64,arm64,ppc64el] http://ftp.utexas.edu/mariadb/repo/10.3/ubuntu bionic main'
    Single line code block on line 240  with contents: sudo apt update
    Single line code block on line 244  with contents: sudo apt install mariadb-server
    Single line code block on line 250  with contents: mysql --version
    Single line code block on line 257  with contents: git clone https://github.com/iotaledger/rpchub.git hub
    Single line code block on line 261  with contents: cd hub
    Single line code block on line 265  with contents: bazel build -c opt //hub:hub
    Single line code block on line 274  with contents: echo "CREATE DATABASE hub" | mysql -uroot -pmyrootpassword
    Single line code block on line 278  with contents: mysql -h127.0.0.1 -uroot -pmyrootpassword hub < schema/schema.sql
    Single line code block on line 282  with contents: mysql -h127.0.0.1 -uroot -pmyrootpassword hub < schema/triggers.mariadb.sql
    Single line code block on line 289  with contents: nano start.sh
    Single line code block on line 319  with contents: chmod a+x start.sh
    Single line code block on line 323  with contents: ./start.sh
  2 multi-line code block(s) found:
    Multiline code block on lines 300-311 with type '<no type>'
      Contents (10 lines):
        #!/bin/bash

        ./bazel-bin/hub/hub \
          --salt REPLACETHIS!!! \
          --db hub \
          --dbUser root \
          --dbPassword myrootpassword \
          --apiAddress 127.0.0.1:14265 \
          --minWeightMagnitude 14 \
          --listenAddress 127.0.0.1:50051
    Multiline code block on lines 313-315 with type 'json'
      Contents (1 line):
                test
  2 unique multi-line code block type(s) found:
    Number of occurences per type:
       <no type>:   1x
            json:   1x
\pages\hub\quick-start\simple-install.md

[ ... ]

Overall stats:
    20 single code block types found in 1 file
    11 multi-line code block types found in 3 files:
       4 unique (multi-line) code block types found:
         3x found in   1 file(s): sh
         1x found in   1 file(s): md
         6x found in   2 file(s): <no type>
         1x found in   1 file(s): json
```
