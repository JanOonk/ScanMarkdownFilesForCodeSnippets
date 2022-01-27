# ScanMarkdownFilesForCodeSnippets
Scans Markdown files and shows stats about codesnippet usage

```console
Syntax: <program> <rootFolderToScan> [filePattern = *.md] [-onlyTopLevel = no]
  For example:
    ScanMarkDownFiles /sources/myrepo
      will scan all /sources/myrepo including subdirs for *.md files

    ScanMarkDownFiles /sources/myrepo *.mdwn
      will scan all /sources/myrepo including subdirs for *.mdwn files

    ScanMarkDownFiles /sources/myrepo *.md -onlyTopLevel
      will only scan the top level directory /sources/myrepo for *.md files
```

Example output:
```console
Found 64 files matching *.md in c:\temp\goshimmer-docs-update-docs-annotations v2
\CHANGELOG.md: 1 codesnippets found
  1 Unique Types: ```diff
  ```diff: 1x
\README.md: 3 codesnippets found
  1 Unique Types: ```bash
  ```bash: 3x
\.github\CODE_OF_CONDUCT.md: 0 codesnippets found
\.github\CONTRIBUTING.md: 0 codesnippets found
\.github\pull_request_template.md: 0 codesnippets found
\.github\SECURITY.md: 0 codesnippets found
\.github\SUPPORT.md: 0 codesnippets found
\documentation\README.md: 8 codesnippets found
  3 Unique Types: ```, ```json, ```shell
  ```: 1x
  ```json: 2x
  ```shell: 5x
  
[...]

\documentation\docs\protocol_specification\components\markers.md: 2 codesnippets found
  1 Unique Types: ```go
  ```go: 2x
\documentation\docs\protocol_specification\components\overview.md: 0 codesnippets found
\documentation\docs\protocol_specification\components\tangle.md: 3 codesnippets found
  1 Unique Types: ```go
  ```go: 3x
\plugins\analysis\dashboard\frontend\README.md: 0 codesnippets found

Overall stats:
 Unique codesnippet types found: 9
     1x found in   1 file(s): ```diff
   503x found in  42 file(s): ```
     3x found in   1 file(s): ```bash
   211x found in  23 file(s): ```shell
    61x found in  22 file(s): ```json
   133x found in  25 file(s): ```go
     2x found in   1 file(s): ```csv
     9x found in   4 file(s): ```yaml
     3x found in   2 file(s): ```vbnet

```
