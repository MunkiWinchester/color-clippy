# Color Clippy

## Debug Squirrel Installation

Copy a update (`Update.exe`) into the `bin`-Folder.

## Release

1. `PackAndReleasify.ps1 --IncreaseVersion`

2. Commit & Push

3. Create a release on GitHub and attach (out of the `Releases` folder)
    - `ColorClippy-X.X.X-full.nupkg`
    - `ColorClippy-X.X.X-delta.nupkg`
    - `RELEASES`
    - `Setup.exe`
    - `Setup.msi`
