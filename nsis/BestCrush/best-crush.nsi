!define Name "BestCrush"
!define DisplayName "Best Crush"
!define AppFile "BestCrush.exe"
!define InstFile "${DisplayName} setup v${Version}.exe"
!define Slug "${DisplayName} v${Version}"
!define Icon "assets\best-crush.ico"
!define UninstName "Uninstall"
!define UninstFile "${UninstName}.exe"

!ifndef Version
    !define Version "0.0.0"
!endif

!include MUI2.nsh
!include LogicLib.nsh

Var PrevInstDir
Var PrevInstFile

Function .onInit

; ---------------------
; Splash screen
; ---------------------
InitPluginsDir
File "/oname=$PluginsDir\splash.bmp" "assets\splash.bmp"

advsplash::show 1400 600 0 -1 $PluginsDir\splash

Pop $0 ; $0 has '1' if the user closed the splash screen early,
     ; '0' if everything closed normally, and '-1' if some error occurred.

${If} $0 < 0
    MessageBox MB_OK|MB_ICONSTOP "An unexpected error has occurred. ($0)"
    Abort
${EndIf}

; ---------------------
; Uninstall previous version
; ---------------------
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
        ${EndIf}
        
        Delete "$PrevInstFile"
        RMDir "$PrevInstDir"
        MessageBox MB_OK|MB_ICONINFORMATION "La désinstallation s'est terminée avec succès."
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

!define MUI_FINISHPAGE_LINK "Best Crush is on GitHub"
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

Section ".NET 10 Runtime" DotNet10Runtime
    SectionIn RO
    SetOutPath "$INSTDIR\Redist"
    File redist\windowsdesktop-runtime-10.0.0-win-x64.exe
    DetailPrint "Running .NET 10 Runtime Setup..."
    ExecWait "$INSTDIR\Redist\windowsdesktop-runtime-10.0.0-win-x64.exe /install /quiet /norestart"
    ${If} $0 <> 0
        MessageBox MB_OK|MB_ICONSTOP ".NET 10 Runtime installation failed. Error code: $0"
        Abort
    ${Else}
        DetailPrint "Finished .NET 10 Runtime Setup"
    ${EndIf}
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
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Name}" "InstallDir" "$INSTDIR"
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

LangString DESC_DotNet10Runtime ${LANG_FRENCH} "Installer le runtime .NET Core 10.0 s'il n'est pas déjà présent."
LangString DESC_BestCrush ${LANG_FRENCH} "Installer Best Crush."

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${DotNet10Runtime} $(DESC_DotNet10Runtime)
  !insertmacro MUI_DESCRIPTION_TEXT ${BestCrush} $(DESC_BestCrush)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

Function CreateDesktopShortCut
    CreateShortCut "$DESKTOP\${DisplayName}.lnk" "$INSTDIR\${AppFile}" "" "${Icon}"
FunctionEnd

Function un.onUninstSuccess
  MessageBox MB_OK|MB_ICONINFORMATION "Désinstallation de ${Slug} terminée." /SD IDOK
FunctionEnd