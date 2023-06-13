# Windows Services Manager

## About
As its name implies, this is a Windows Service app that manages workers (or sub-services).
<br />DLLs can be created in a way that this manager can dynamic load them.
<br />Classes that extends the ServiceWorker class are then executed by its own thread
```
public class Worker1 : ServiceWorker
{
        public override string Title
        {
            get { return "Worker Tester 1"; }
        }

        public override void Run()
        {
            Logger.LogInfo("starting working");
            ...
            Logger.LogInfo("work is done");
        }

        public override void Stop()
        {
            ...
        }
}
```
WServicesManager is the Windows Services Manger 

##### WServicesManager.exe --help

![](/Docs/help.png "Service help message")

##### WServicesManager.exe --configure

![](/Docs/configuration.png "Service configuration")

##### WServicesManager.exe --config c:\temp\tsvc.cfg --install

![](/Docs/installation.png "Service installation")

![](/Docs/control_panel.png "Service control")

##### WServicesManager.exe --config c:\temp\tsvc.cfg --debug

![](/Docs/debug.png "Service debug mode")

CSevicesManger is the console version with less functionalities

##### CServicesManager.exe --help

![](/Docs/help_console.png "Service help message")

##### CServicesManager.exe --config c:\temp\tsvc.cfg --debug

![](/Docs/debug_console.png "Service debug mode")

## Execution Log
```
2023-06-12 22:48:50  I  Services  Logger successfully initialized
2023-06-12 22:48:50  I  Services  Loading dll "F:\temp\WSvc\workers\Dependency.dll"
2023-06-12 22:48:50  I  Services  Loading dll "F:\temp\WSvc\workers\Worker1.dll"
2023-06-12 22:48:50  I  Services  Loading dll "F:\temp\WSvc\workers\Worker2.dll"
2023-06-12 22:48:50  I  Services  Loading dll "F:\temp\WSvc\workers\Worker3.dll"
2023-06-12 22:48:50  I  Services  Starting service 'Worker Tester 1'
2023-06-12 22:48:50  I  Worker1  starting working
2023-06-12 22:48:50  I  Worker1  performing some work...
2023-06-12 22:48:50  I  Services  Starting service 'Worker Tester 2'
2023-06-12 22:48:50  I  Worker2  starting working
2023-06-12 22:48:50  I  Worker2  performing some work...
2023-06-12 22:48:50  I  Services  Starting service 'Worker Tester 3'
2023-06-12 22:48:50  I  Worker3  starting working
2023-06-12 22:48:50  I  Worker3  performing some work...
2023-06-12 22:48:50  I  Services  Starting service 'Staging Watcher'
2023-06-12 22:49:04  I  Worker1  performing some work...
2023-06-12 22:49:04  I  Worker2  performing some work...
2023-06-12 22:49:18  I  Worker2  performing some work...
...
2023-06-12 22:51:10  I  Worker1  performing some work...
2023-06-12 22:51:15  I  Services  Asking service 'Worker Tester 1' to stop
2023-06-12 22:51:15  I  Services  Asking service 'Worker Tester 2' to stop
2023-06-12 22:51:15  I  Services  Asking service 'Worker Tester 3' to stop
2023-06-12 22:51:15  I  Services  Asking service 'Staging Watcher' to stop
2023-06-12 22:51:15  I  Services  Waiting service 'Worker Tester 1' to stop
2023-06-12 22:51:15  I  Worker1  work is done
2023-06-12 22:51:15  I  Services  Service 'Worker Tester 1' stopped
2023-06-12 22:51:15  I  Services  Waiting service 'Worker Tester 2' to stop
2023-06-12 22:51:15  I  Worker2  work is done
2023-06-12 22:51:15  I  Services  Service 'Worker Tester 2' stopped
2023-06-12 22:51:15  I  Services  Waiting service 'Worker Tester 3' to stop
2023-06-12 22:51:15  I  Worker3  work is done
2023-06-12 22:51:15  I  Services  Service 'Worker Tester 3' stopped
2023-06-12 22:51:15  I  Services  Waiting service 'Staging Watcher' to stop
2023-06-12 22:51:15  I  Services  Service 'Staging Watcher' stopped

```
