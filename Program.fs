namespace TextEditorApp

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Themes.Fluent
open Avalonia.FuncUI.Hosts
open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Layout

module Main =

    // Define the view function to create the UI
    let view () =
        Component(fun ctx ->
            // Use a state to manage the text in the TextBox
            let textState = ctx.useState ""
            printf "%A" textState.Current

            // Create a DockPanel layout
            DockPanel.create [
                DockPanel.children [
                    // Create a TextBox for text input
                    TextBox.create [
                        TextBox.dock Dock.Top
                        TextBox.text textState.Current
                        TextBox.onTextChanged (fun newText -> textState.Set(newText))
                        TextBox.horizontalAlignment HorizontalAlignment.Stretch
                        TextBox.verticalAlignment VerticalAlignment.Stretch
                        TextBox.acceptsReturn true
                    ]
                ]
            ]
        )

type MainWindow() =
    inherit HostWindow()
    do
        base.Title <- "Text Editor Example"
        base.Content <- Main.view ()

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add (FluentTheme())
        this.RequestedThemeVariant <- Styling.ThemeVariant.Dark

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

module Program =

    [<EntryPoint>]
    let main(args: string[]) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)
