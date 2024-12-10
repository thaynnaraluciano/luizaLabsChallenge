using Infrastructure.Services.Interfaces.v1;

namespace Infrastructure.Services.Services.v1
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string GenerateConfirmationEmail(string receiverName)
        {
            return $@"
            <html>
                <head>
                    <style>
                        body {{
                            display: flex;
                            justify-content: center;
                            align-items: center;
                            height: 100vh;
                            margin: 0;
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            text-align: center;
                            background-color: white;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            width: 100%;
                            max-width: 400px;
                        }}
                        button {{
                            background-color: #007BFF;
                            color: white;
                            border: none;
                            padding: 10px 20px;
                            font-size: 16px;
                            border-radius: 5px;
                            cursor: pointer;
                            transition: background-color 0.3s;
                        }}
                        button:hover {{
                            background-color: #0056b3;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <h1>{receiverName},</h1>
                        <h2>Confirme seu email, clicando no link abaixo:</h2>
                        <a>
                            <button>Confirmar Email</button>
                        </a>
                    </div>
                </body>
            </html>";
        }
    }
}
