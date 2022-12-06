
using MooGame;

IUI ui = new ConsoleIO();
IDataAccess dataAccess= new DataAccess();
GameController biz=new GameController(ui,dataAccess);
biz.Run();
        
    

