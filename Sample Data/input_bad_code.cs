using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EnterpriseApp.Services
{
    public class PaymentProcessor
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();
        
        public async Task ProcessUserPaymentsAsync(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            LogMessage($"Starting payment processing for {user.Email}"); // Will throw NullReferenceException!

            var pendingInvoices = _dbContext.Invoices.Where(i => i.UserId == userId && !i.IsPaid).ToList();

            foreach (var invoice in pendingInvoices)
            {
                try
                {
                    // Thread blocking!
                    var paymentResult = ProcessPaymentExternalAsync(invoice.Amount).Result; 

                    if (paymentResult.Success)
                    {
                        invoice.IsPaid = true;
                        // Fire and forget without await
                        NotifyUserAsync(user.Id, invoice.Id); 
                    }
                }
                catch (Exception ex)
                {
                    LogMessage($"Failed: {ex.Message}");
                }
            }
            _dbContext.SaveChangesAsync().Wait();
        }

        private async Task<PaymentResponse> ProcessPaymentExternalAsync(decimal amount)
        {
            await Task.Delay(1000);
            return new PaymentResponse { Success = true };
        }

        private async void NotifyUserAsync(int userId, int invoiceId)
        {
            await Task.Delay(500); 
            LogMessage($"User {userId} notified");
        }

        private void LogMessage(string message)
        {
            System.IO.File.AppendAllText("payment_logs.txt", $"{DateTime.Now}: {message}\n");
        }
    }

    public class DatabaseContext { 
        public List<User> Users { get; set; } = new List<User>(); 
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public Task SaveChangesAsync() => Task.CompletedTask;
    }
    public class User { public int Id { get; set; } public string Email { get; set; } }
    public class Invoice { public int Id { get; set; } public int UserId { get; set; } public decimal Amount { get; set; } public bool IsPaid { get; set; } }
    public class PaymentResponse { public bool Success { get; set; } }
}
