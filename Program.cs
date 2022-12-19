
using MooGame;

IUI ui = new ConsoleIO();
IDataAccess dataAccess= new DataAccess();
GameController moGameController=new GameController(ui,dataAccess);
moGameController.Run();
        
    

