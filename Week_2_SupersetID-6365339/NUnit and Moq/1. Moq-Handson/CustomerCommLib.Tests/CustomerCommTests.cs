using Moq;
using CustomerCommLib;

namespace CustomerCommLib.Tests;

public class CustomerCommTests
{
    private Mock<IMailSender> _mockMailSender;
    private CustomerComm _customerComm;

    [SetUp]
    public void Setup()
    {
        _mockMailSender = new Mock<IMailSender>();
        
        _mockMailSender.Setup(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);
        
        _customerComm = new CustomerComm(_mockMailSender.Object);
    }

    [Test]
    public void SendMailToCustomer_ShouldReturnTrue()
    {
        bool result = _customerComm.SendMailToCustomer();
        
        Assert.That(result, Is.True, "SendMailToCustomer should return true");
    }
    
    [Test]
    public void SendMailToCustomer_ShouldCallSendMail()
    {
        _customerComm.SendMailToCustomer();
        
        _mockMailSender.Verify(m => m.SendMail("cust123@abc.com", "Some Message"), Times.Once);
    }
}
