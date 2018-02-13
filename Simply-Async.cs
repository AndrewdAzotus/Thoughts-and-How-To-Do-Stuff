/* * * * * * * * * * * * * * * * * * * * * * * *
 * the simplest async example  *
 *                             *
 * this was to allow a async   *
 * task to be started but the  *
 * program not to exit until   *
 * the task was terminated     *
 * early in a clean manner or  *
 * completed naturally.        *
 * * * * * * * * * * * * * * * *
 * in MVVM                     *
 * * * * * * * * * * * * * * * * * * * * * * * */

// somewhere to store the task identifier [in the model?]
private Task _Tsk;
public Task Tsk { get => _Tsk; set => _Tsk = value; }
private bool _CancelScanning = false;
public bool CancelScanning { get => _CancelScanning; set => _CancelScanning = value; }

// kick off the aSync task [in the ViewModel, 
// possibly by a View form button]
// note, there is no async in the method defn
public void cmdAsyncDataScan()
{
    Tsk = Task.Run(() => cmdDataScan());
}

public void cmdDataScan()
{
    // big loop scanning data or table or file or something...
    // e.g.
    foreach (string fileName in Directory.EnumerateFiles(pathToFiles))
    {
        // read in file and do what-not
        
        if (CancelScanning)
            break;
            
    }
}

// code for the exit button, note async is only needed here
public async void cmdExit()
{
    if (!CanExecScanForNewTags)
    {
        LblExitText = "Exiting...";  // replaces the exit btn text to let the user know the button press has been acknowledged
        CanExecExit = false;         // disable the exit btn
        if (Tsk != null)
        {
            CancelScanning = true;
            await ScanTask;          // wait for background task to complete
        }
    }
    
    SaveEverything();                // optional save stuff, to be done regardless
    Environment.Exit(0);
}
