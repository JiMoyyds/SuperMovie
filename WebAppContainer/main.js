// Modules to control application life and create native browser window
const {app, shell, BrowserWindow, Menu} = require('electron')
const fs = require('fs')

//hide the menu
Menu.setApplicationMenu(null)

function createWindow() {
    // Create the browser window.
    /*const mainWindow = new BrowserWindow({
      width: 800,
      height: 600,
      webPreferences: {
        preload: path.join(__dirname, 'preload.js')
      }
    })*/

    // and load the index.html of the app.
    //mainWindow.loadFile('index.html')

    let runPath//path of exec file
    if (!app.isPackaged)
        //The length of str 'node_modules' is 12
        //runPath = app.getPath("exe").substring(0, app.getPath("exe").length - app.getName().length - 12)//dev
        runPath = app.getPath("exe").substring(0, app.getPath("exe").length - "node_modules/electron/dist/electron".length)
    else
        runPath = app.getPath("exe").substring(0, app.getPath("exe").length - app.getName().length)//prod


    let appconfig_path = `${runPath}/appconfig.json`

    let appconfig
    try {
        appconfig = JSON.parse(fs.readFileSync(appconfig_path, 'utf8'))
    } catch (e) {
        console.log(e)
    }

    mainWindow = new BrowserWindow({
        width: appconfig.width,
        height: appconfig.height,
        resizable: appconfig.resizable,
        show: false
    })

    // Open the DevTools.
    /*
    if (appconfig.dev)
        mainWindow.webContents.openDevTools()*/

    //using system default browser to open link
    mainWindow.webContents.setWindowOpenHandler(details =>
        shell.openExternal(details.url)
    )

    mainWindow.webContents.on("dom-ready", async function () {
        appconfig.css.forEach(path =>
            mainWindow.webContents.insertCSS(fs.readFileSync(path, 'utf-8'))
        )
    });

    mainWindow.webContents.on("did-finish-load", async function () {
        mainWindow.show()
    });

    mainWindow.loadURL(appconfig.stat)//restore navi status

    /*
    mainWindow.on('close', () => {
        const url = mainWindow.webContents.getURL()
        //save url to status when still in to-do
        if (url.startsWith(appconfig.base)) {
            appconfig.stat = url
            fs.writeFileSync(appconfig_path, appconfig, "utf8")
        }
    })*/
}

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.whenReady().then(() => {
    createWindow()

    app.on('activate', function () {
        // On macOS it's common to re-create a window in the app when the
        // dock icon is clicked and there are no other windows open.
        if (BrowserWindow.getAllWindows().length === 0) createWindow()
    })
})

// Quit when all windows are closed, except on macOS. There, it's common
// for applications and their menu bar to stay active until the user quits
// explicitly with Cmd + Q.
app.on('window-all-closed', function () {
    if (process.platform !== 'darwin') app.quit()
})

// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and require them here.
