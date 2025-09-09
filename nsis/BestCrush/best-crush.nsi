!define Name "BestCrush"
!define DisplayName "Best Crush"
!define Version "__VERSION__"
!define AppFile "BestCrush.exe"
!define InstFile "${DisplayName} setup v${Version}.exe"
!define Slug "${DisplayName} v${Version}"
!define Icon "assets\best-crush.ico"
!define UninstName "Uninstall"
!define UninstFile "${UninstName}.exe"

!include MUI2.nsh
!include LogicLib.nsh

; ---------------------
; Uninstall previous version
; ---------------------

Var PrevInstDir
Var PrevInstFile

Function .onInit
ReadRegStr $PrevInstDir HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Name}" "InstallDir"
ReadRegStr $PrevInstFile HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Name}" "UninstallString"
${If} $PrevInstDir != "" 
${AndIf} $PrevInstFile != ""
    ${If} ${Cmd} `MessageBox MB_YESNO|MB_ICONEXCLAMATION "Une version précédente de ${Name} a été détectée sur votre système, elle doit être désinstallée avant de continuer." /SD IDYES IDYES`
        ExecWait '"$PrevInstFile" /S _?=$PrevInstDir'
        Pop $0
        
        ${If} $0 <> 0
            MessageBox MB_YESNO|MB_ICONSTOP "La désinstallation a échoué. Continuer quand même ?" /SD IDYES IDYES +2
                Abort
        ${Else}
            MessageBox MB_OK|MB_ICONINFORMATION "La désinstallation s'est terminée avec succès."
        ${EndIf}
    ${Else}
        Abort
    ${EndIf}
${EndIf}
FunctionEnd

; ---------------------
; Installer
; ---------------------

Name "${DisplayName}"
Outfile "${InstFile}"
InstallDir "$ProgramFiles\${DisplayName}"
InstallDirRegKey HKCU "Software\DofusSharp\${Name}" ""

!define MUI_ICON "${Icon}"
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "assets\head.bmp"
!define MUI_ABORTWARNING
!define MUI_WELCOMEPAGE_TITLE "${SLUG} Setup"

!define MUI_FINISHPAGE_RUN "$INSTDIR\${AppFile}"

!define MUI_FINISHPAGE_SHOWREADME
!define MUI_FINISHPAGE_SHOWREADME_TEXT "Create Desktop Shortcut"
!define MUI_FINISHPAGE_SHOWREADME_FUNCTION CreateDesktopShortCut
!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED

!define MUI_FINISHPAGE_LINK "Best Crush Source Code"
!define MUI_FINISHPAGE_LINK_LOCATION "https://github.com/ismailbennani/DofusSharp/tree/main/BestCrush"

!macro MUI_FINISHPAGE_SHORTCUT
 
  !ifndef MUI_FINISHPAGE_NOREBOOTSUPPORT
    !define MUI_FINISHPAGE_NOREBOOTSUPPORT
    !ifdef MUI_FINISHPAGE_RUN
      !undef MUI_FINISHPAGE_RUN
    !endif
  !endif
  !define MUI_PAGE_CUSTOMFUNCTION_SHOW DisableCancelButton
  !insertmacro MUI_PAGE_FINISH
  !define MUI_PAGE_CUSTOMFUNCTION_SHOW DisableBackButton
 
  Function DisableCancelButton
    EnableWindow $mui.Button.Cancel 0
  FunctionEnd
 
  Function DisableBackButton
    EnableWindow $mui.Button.Back 0
  FunctionEnd
 
!macroend

Var SMDir ;Start menu folder

; Installer
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "license.txt"
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY

!define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
!define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\DofusSharp\${Name}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
!insertmacro MUI_PAGE_STARTMENU 0 $SMDir

!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

; Uninstaller
!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

; Language
!insertmacro MUI_LANGUAGE "French"

Section "Windows App SDK" WindowsAppSDK
    SectionIn RO
    SetOutPath "$INSTDIR\Redist"
    File "redist\WindowsAppRuntimeInstall-x64.exe"
    DetailPrint "Running Windows App SDK Setup..."
    ExecWait "$INSTDIR\Redist\WindowsAppRuntimeInstall.exe --quiet"
    DetailPrint "Finished Windows App SDK Setup"
SectionEnd

Section "Best Crush" BestCrush
    SectionIn RO
    SetOutPath "$INSTDIR"
    File /r app\*.*
    WriteRegStr HKCU "Software\DofusSharp\${Name}" "" $INSTDIR
    WriteRegStr HKLM "Software\DofusSharp\${Name}" "Version" "${Version}"
    WriteUninstaller "$INSTDIR\${UninstFile}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Name}" "DisplayName" "${DisplayName}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Name}" "DisplayIcon" "${Icon}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Name}" "InstallDir" "$INSTDIR\${UninstFile}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Name}" "UninstallString" "$INSTDIR\${UninstFile}"
SectionEnd

Section -StartMenu
    !insertmacro MUI_STARTMENU_WRITE_BEGIN 0 
    CreateDirectory "$SMPrograms\$SMDir"
    CreateShortCut "$SMPrograms\$SMDir\${DisplayName}.lnk" "$INSTDIR\${AppFile}" "" "${Icon}"
    CreateShortCut "$SMPrograms\$SMDir\${UninstName}.lnk" "$INSTDIR\${UninstFile}"
    !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section "Uninstall"
    RMDir /r "$APPDATA\BestCrush"
    RMDir /r "$LOCALAPPDATA\BestCrush"
    !insertmacro MUI_STARTMENU_GETFOLDER 0 $SMDir
    RMDir /r "$SMPrograms\$SMDir"
    Delete "$DESKTOP\${DisplayName}.lnk"
    DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Name}"
    DeleteRegKey HKCU "Software\DofusSharp\${Name}"
    RMDir /r "$INSTDIR"
SectionEnd

LangString DESC_WindowsAppSDK ${LANG_FRENCH} "Installer le runtime du Kit de développement logiciel (SDK) d’application Windows."
LangString DESC_BestCrush ${LANG_FRENCH} "Installer Best Crush."

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${WindowsAppSDK} $(DESC_WindowsAppSDK)
  !insertmacro MUI_DESCRIPTION_TEXT ${BestCrush} $(DESC_BestCrush)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

Function CreateDesktopShortCut
    CreateShortCut "$DESKTOP\${DisplayName}.lnk" "$INSTDIR\${AppFile}" "" "${Icon}"
FunctionEnd

Function un.onUninstSuccess
  MessageBox MB_OK|MB_ICONINFORMATION "${Slug} uninstall complete.cd" /SD IDOK
FunctionEnd