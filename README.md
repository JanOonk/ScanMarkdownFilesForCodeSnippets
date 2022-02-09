# Scan Markdown Files For CodeBlocks
Scans Markdown files for inline and fenced codeblocks, ability to search/replace codeblock type, and shows stats about codeblocks usage and what syntax (json, console, sh, javascript, md, ...) was used.
It's extremely fast and uses the CommonMark markdown compliant MarkDig library.

For syntax on how to make (fenced) codeblocks and use syntax highlighting see https://www.markdownguide.org/extended-syntax/#fenced-code-blocks and https://www.markdownguide.org/extended-syntax/#syntax-highlighting

## How to use

Just run the executable and it will show all the options and help:
```console
Copyright (C) 2022 ScanMarkdownFiles

  --rootfolder              Required. Rootfolder to scan

  --filepattern             (Default: *.md) Files to scan

  --includesubfolders       (Default: true) Scan also subfolders of rootfolder

  --showinlinecodeblocks    (Default: true) Show inline code blocks in output

  --showfencedcodeblocks    (Default: true) Show fenced code blocks in output

  -r, --replacetype         Replace one or more codeblock type with another type for example:
                            replace 'bash' and 'sh' with 'shell': -r bash=shell sh=shell

  --help                    Display this help screen.

  --version                 Display version information.
```

Example output for `ScanMarkdownFiles.exe --rootfolder c:\temp\iota-repos`:
```console
Found 852 files matching '*.md' in 'c:\temp\iota-repos':

[...]

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

[...]

\wasp-master\packages\vm\wasmlib\ts\wasmclient\buffer\readme.md
\wasp-master\contracts\wasm\fairroulette\frontend\src\lib\fairroulette_client\readme.md
\wasp-master\contracts\wasm\fairroulette\frontend\src\lib\wasp_client\readme.md
\wasp-master\contracts\wasm\fairroulette\frontend\src\lib\wasp_client\buffer\readme.md

Overall stats:
    10 inline code block types found in 6 files
  1555 fenced code block types found in 270 files:
      28 unique fenced code block types found:
       405x found in  92 file(s): <no type>
       133x found in  47 file(s): bash
         3x found in   1 file(s): bash=
        10x found in   2 file(s): c
        68x found in  14 file(s): console
         2x found in   1 file(s): csv
         1x found in   1 file(s): diff
       155x found in  45 file(s): go
         8x found in   2 file(s): java
        22x found in   9 file(s): javascript
         1x found in   1 file(s): javascript=
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

Finished in 00:00:06.1708832
```

Example output for `ScanMarkdownFiles.exe --rootfolder c:\temp\iota-repos --replacetype yml=yaml bash=shell`:
```console
Found 805 files matching '*.md' in 'c:\temp\iota-repos':

\README.md
\bee-dev\README.md

[...]

\wasp-master\documentation\docs\guide\evm\examples\introduction.md
  1 fenced code block(s) found:
    Fenced code block on lines 15-35 with type 'solidity'
      Contents (19 lines):
        pragma solidity ^0.8.6;
        // No SafeMath needed for Solidity 0.8+
        
        contract Counter { 
           
            uint256 private _count;
                
            function current() public view returns (uint256) {
                return _count;
            }   
        
            function increment() public {
                _count += 1;
            }   
        
            function decrement() public {
                _count -= 1;
            }   
        }
  1 unique fenced code block type(s) found:
    Number of occurences per type:
        solidity:   1x
\wasp-master\packages\vm\wasmlib\ts\wasmclient\buffer\readme.md
\wasp-master\contracts\wasm\fairroulette\frontend\src\lib\fairroulette_client\readme.md
\wasp-master\contracts\wasm\fairroulette\frontend\src\lib\wasp_client\readme.md
\wasp-master\contracts\wasm\fairroulette\frontend\src\lib\wasp_client\buffer\readme.md

Overall stats:
    10 inline code block types found in 6 files
  1554 fenced code block types found in 269 files:
      28 unique fenced code block types found:
       404x found in  91 file(s): <no type>
       133x found in  47 file(s): bash
         3x found in   1 file(s): bash=
        10x found in   2 file(s): c
        68x found in  14 file(s): console
         2x found in   1 file(s): csv
         1x found in   1 file(s): diff
       155x found in  45 file(s): go
         8x found in   2 file(s): java
        22x found in   9 file(s): javascript
         1x found in   1 file(s): javascript=
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

Replacing codeblock types:
  'yml' with 'yaml'
       2x in \wasp-master\documentation\docs\guide\chains_and_nodes\running-a-node.md
    Total replacements: 2x
  'bash' with 'shell'
       6x in \chronicle.rs-main\README.md
       1x in \explorer-dev\README.md
       1x in \iota.go-main\README.md
       3x in \streams-develop\README.md
       3x in \cxc-slu-authenticity-main\authenticity-collector\README.md
       5x in \cxc-slu-authenticity-main\ssi-proof-of-ownership\README.md
       1x in \bee-dev\documentation\docs\configuration.md
       6x in \chronicle.rs-main\documentation\docs\getting_started.md
       4x in \chrysalis-docs-main\docs\firefly\verify_download.md
       3x in \chrysalis-docs-main\docs\guides\exchange.md
       9x in \firefly-develop\packages\desktop\README.md
       1x in \firefly-develop\packages\mobile\README.md
       7x in \identity.rs-dev\bindings\wasm\README.md
       3x in \integration-services-master\clients\node\README.md
       3x in \iota.rs-dev\bindings\nodejs\README.md
       3x in \iota.rs-dev\bindings\wasm\README.md
       5x in \streams-develop\bindings\wasm\README.md
       1x in \integration-services-master\documentation\docs\examples\authorize-to-channel.md
       1x in \integration-services-master\documentation\docs\examples\create-channel.md
       1x in \integration-services-master\documentation\docs\examples\delete-users.md
       1x in \integration-services-master\documentation\docs\examples\search-channel-and-validate-data.md
       1x in \integration-services-master\documentation\docs\examples\trusted-authorities.md
       1x in \integration-services-master\documentation\docs\examples\update-users.md
      12x in \wasp-master\contracts\wasm\erc721\README.md
       1x in \firefly-develop\packages\backend\bindings\c\README.md
       1x in \firefly-develop\packages\backend\bindings\capacitor\README.md
       3x in \identity.rs-dev\documentation\docs\libraries\rust\getting_started.md
       2x in \identity.rs-dev\documentation\docs\libraries\wasm\cheat_sheet.md
       4x in \iota.rs-dev\documentation\docs\libraries\java\examples.md
       1x in \iota.rs-dev\documentation\docs\libraries\java\getting_started.md
       3x in \iota.rs-dev\documentation\docs\libraries\nodejs\api_reference.md
       2x in \iota.rs-dev\documentation\docs\libraries\python\getting_started.md
       1x in \iota.rs-dev\documentation\docs\libraries\rust\api_reference.md
       1x in \iota.rs-dev\documentation\docs\libraries\rust\getting_started.md
       3x in \iota.rs-dev\documentation\docs\libraries\wasm\getting_started.md
       1x in \streams-develop\documentation\docs\libraries\c\getting_started.md
       1x in \streams-develop\documentation\docs\libraries\rust\getting_started.md
       2x in \wallet.rs-dev\documentation\docs\libraries\python\getting_started.md
       6x in \wasp-master\documentation\docs\guide\chains_and_nodes\running-a-node.md
       3x in \wasp-master\documentation\docs\guide\evm\create-chain.md
       1x in \wasp-master\documentation\docs\guide\evm\tooling.md
       5x in \wasp-master\documentation\docs\guide\example_projects\fair_roulette.md
       1x in \integration-services-master\documentation\docs\getting_started\installation\docker_compose\docker_compose.md
       1x in \integration-services-master\documentation\docs\getting_started\installation\docker_compose\expose_apis.md
       2x in \integration-services-master\documentation\docs\getting_started\installation\kubernetes\configuration.md
       5x in \integration-services-master\documentation\docs\getting_started\installation\kubernetes\expose_apis.md
       1x in \integration-services-master\documentation\docs\getting_started\installation\kubernetes\local_setup.md
    Total replacements: 133x
  Overall total replacements: 135x

Finished in 00:00:00.6264852
```
