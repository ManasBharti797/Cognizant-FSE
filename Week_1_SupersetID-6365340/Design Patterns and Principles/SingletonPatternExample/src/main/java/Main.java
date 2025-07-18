
public class Main {
    public static void main(String[] args) {
        System.out.println("=== Singleton Pattern Example ===\n");
        
        // Run the Singleton test
        SingletonTest.main(args);
        
        System.out.println("\n=== Additional Singleton Demo ===");
        
        // Demonstrate that the Logger maintains state across different accesses
        Logger appLogger = Logger.getInstance();
        appLogger.log("Application started");
        
        // Simulate getting logger from different parts of the application
        simulateModuleA();
        simulateModuleB();
        
        appLogger.log("Application finished");
    }
    
    private static void simulateModuleA() {
        Logger moduleALogger = Logger.getInstance();
        moduleALogger.log("Module A is executing");
    }
    
    private static void simulateModuleB() {
        Logger moduleBLogger = Logger.getInstance();
        moduleBLogger.log("Module B is executing");
    }
}
