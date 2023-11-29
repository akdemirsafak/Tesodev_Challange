using Microsoft.Extensions.Options;
using Order.Service.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Order.Service.EmailServices;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }
    public async Task SendDailyOrderLogs(SendDailyOrderLogsModel sendDailyOrderLogsModel)
    {
        var smtpClient=new SmtpClient();
        smtpClient.Host = _emailSettings.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);

        var mailMessage= new MailMessage();
        mailMessage.From = new MailAddress(_emailSettings.Email);

        mailMessage.To.Add(sendDailyOrderLogsModel.To);
        
        mailMessage.Subject = sendDailyOrderLogsModel.subject;
        var stringBuilder= new StringBuilder();
        mailMessage.Body = @$"
            <html>
                <body>
                    <h5> Daily Order Logs : </h5>
                    </br></br> 
                    <table>
                        <thead>
                            <tr>
                            <td>Order Id</td>
                            <td>Product Name</td>
                            <td>Quantity</td>
                            <td>Status</td>
                            <td>Price</td>
                            <td>Address</td>
                            </tr>
                        </thead>
                    <tbody>
                    ";
        foreach (var orderlog in sendDailyOrderLogsModel.logs)
        {
            mailMessage.Body += @$"
                <tr>
                    <td>{orderlog.Id}</td>
                    <td>{orderlog.Product.Name}</td>
                    <td>{orderlog.Quantity}</td>
                    <td>{orderlog.Status}</td>
                    <td>{orderlog.Price}</td>
                    <td>{orderlog.Adress.City+orderlog.Adress.Line}</td>
                </tr>";
        }
        mailMessage.Body += @$"
                    </tbody>
                </table>
            </body>
        </html>";

        mailMessage.IsBodyHtml = true;

        await smtpClient.SendMailAsync(mailMessage);
    }
}
