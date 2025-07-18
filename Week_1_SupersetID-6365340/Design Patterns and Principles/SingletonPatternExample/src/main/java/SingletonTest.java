
public class SingletonTest {
    public static void main(String[] args) {
        System.out.println("=== Testing Singleton Pattern ===");
        
        // Get first instance of Logger
        Logger logger1 = Logger.getInstance();
        logger1.log("First log message");
        System.out.println("Logger1 info: " + logger1.getInstanceInfo());
        
        // Get second instance of Logger
        Logger logger2 = Logger.getInstance();
        logger2.log("Second log message");
        System.out.println("Logger2 info: " + logger2.getInstanceInfo());
        
        // Check if both references point to the same instance
        if (logger1 == logger2) {
            System.out.println("✓ SUCCESS: Both references point to the same instance");
        } else {
            System.out.println("✗ FAILURE: Different instances were created");
        }
        
        // Test from different parts of the application
        testFromAnotherMethod();
    }
    
    private static void testFromAnotherMethod() {
        System.out.println("\n=== Testing from another method ===");
        Logger logger3 = Logger.getInstance();
        logger3.log("Log message from another method");
        System.out.println("Logger3 info: " + logger3.getInstanceInfo());
    }
}
