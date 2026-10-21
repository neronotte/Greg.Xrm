# Installing Red.RoleEditor

Red.RoleEditor is a build of [_n.RoleEditor](https://github.com/neronotte/Greg.Xrm) by Riccardo Gregori
that fixes saving security roles. Saving currently fails in the original tool with:

```
Message name 'AddPrivilegesRole' does not exist.
```

This build installs **next to** the original tool. You can keep `_n.RoleEditor` installed.

## Install

1. Download `Red.RoleEditor.dll` from the [latest release](https://github.com/MichelSteen/n.Roleditor.Xrm/releases/latest).
2. Close XrmToolBox.
3. Copy `Red.RoleEditor.dll` to:

   ```
   %APPDATA%\MscrmTools\XrmToolBox\Plugins
   ```

   Use the `Plugins` folder, **not** `NugetPlugins`. Paste the path above in the Explorer address bar to open it.

4. Unblock the file. Windows marks downloaded DLLs as coming from the internet, and XrmToolBox won't load them.
   Right-click the file > **Properties** > tick **Unblock** > **OK**, or run in PowerShell:

   ```powershell
   Unblock-File "$env:APPDATA\MscrmTools\XrmToolBox\Plugins\Red.RoleEditor.dll"
   ```

5. Start XrmToolBox and open **Red.RoleEditor**.

## How to check it works

Open the **Output** pane in Red.RoleEditor and save a role change. You should see:

```
ExecuteTransaction rejected by the server (Message name 'AddPrivilegesRole' does not exist.). Falling back to sequential execution of 1 request(s): atomicity is lost.
Fallback mode: 1 request(s) executed one by one.
```

followed by **Role saved successfully!**

In this mode, the changes of one save are applied one by one instead of all at once.
If one of them fails, the notification starts with **Role partially saved** and the Output pane lists
which changes were applied (`[applied]`), which one failed (`[failed]`) and which were skipped (`[not executed]`).
Reload the role before editing it again.

## Update

Close XrmToolBox, replace `Red.RoleEditor.dll` with the new version and unblock it again.
The XrmToolBox Tool Library does **not** update this tool.

## Uninstall

Close XrmToolBox and delete `%APPDATA%\MscrmTools\XrmToolBox\Plugins\Red.RoleEditor.dll`.

## Notes

- Red.RoleEditor has its own settings, separate from `_n.RoleEditor`.
- The fix has been submitted upstream. Once it is released in `_n.RoleEditor`, you can switch back to the original tool and uninstall this one.
