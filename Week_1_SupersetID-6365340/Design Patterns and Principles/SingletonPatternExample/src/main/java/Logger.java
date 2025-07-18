
public class Logger {
    // Private static instance of the Logger class
    private static Logger instance;
    
    // Private constructor to prevent instantiation from outside
    private Logger() {
        System.out.println("Logger instance created");
    }
    
    // Public static method to get the single instance of Logger
    public static Logger getInstance() {
        if (instance == null) {
            instance = new Logger();
        }
        return instance;
    }
    
    // Method to log messages
    public void log(String message) {
        System.out.println("[LOG] " + message);
    }
    
    // Method to get the hash code of the instance for testing purposes
    public String getInstanceInfo() {
        return "Logger instance: " + this.hashCode();
    }
}
