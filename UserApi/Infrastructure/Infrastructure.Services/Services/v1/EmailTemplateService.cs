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
                        .footer {{
                            font-size: 12px;
                            color: #666;
                            margin-top: 20px;
                            text-align: center;
                         }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <h1>{receiverName},</h1>
                        <p>Confirme seu email, clicando no link abaixo:</p>
                        <a>
                            <button>Confirmar Email</button>
                        </a>
                    </div>
                    <div class=""footer"">
                        <p>
                            Este e-mail e seus anexos são confidenciais e destinados exclusivamente ao(s) destinatário(s) indicado(s).
                            Se você recebeu esta mensagem por engano, por favor, informe o remetente imediatamente e exclua o e-mail.
                            É proibida qualquer divulgação, cópia ou uso não autorizado do conteúdo.
                        </p>
                        <p>
                            <strong>Contato:</strong> luizalabs@example.com | Telefone: +55 99 99999-9999<br>
                        </p>
                    </div>
                </body>
            </html>";
        }
    }
}
