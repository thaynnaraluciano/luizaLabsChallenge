# LuizaLabs Challenge

Esta aplicação é um desafio técnico da empresa LuizaLabs e consiste em uma tela de login com as funcionalidades de cadastro de usuário, avaliação de segurança de senha, controle de logs e tratamento de exceções.

![image](https://github.com/user-attachments/assets/42de2014-6ce0-495e-990a-cfa2d3a9edc1)

![image](https://github.com/user-attachments/assets/d59cc38a-05c9-4cec-a887-1cf53f39293f)

## O desafio
### Tela de Login
* Permitir que o usuário insira seu nome de usuário e senha
* Realizar validações de entrada para garantir dados corretos
* Autenticar usuário no sistema
* Apresentar mensagens de erro ou sucesso de acordo com o resultado da autenticação

### Cadastro de usuário
* Permitir que o usuário se cadastre fornecendo nome, e-mail e senha
* Realizar validações de entrada para garantir dados corretos
* Avaliar a segurança da senha e apresentar um indicador de nível de segurança
* Persistir a informação do usuário no banco de dados
* Utilizar criptografia para armazenar a senha
* Enviar e-mail de confirmação de cadastro. O usuário só poderá fazer login após a confirmação

### Controle de Logs e Exceções
* Implementar um sistema de controle de logs para registrar eventos importantes na aplicação
* Adicionar tratamento de exceções para lidar com situações inesperadas de forma adequada
* Os logs devem conter informações relevantes para identificação e resolução de problemas

#### As interações entre o frontend e o backend devem ser feitas de forma RESTful

## Dependências
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [WSL - Windows Subsystem for Linux](https://learn.microsoft.com/pt-br/windows/wsl/install)

## Instruções 
Para executar a aplicação em ambiente local, siga os passos a seguir: 

* Após a instalação das dependências, execute o Docker Desktop
* Faça o clone do repositório para um diretório da sua máquina
    ```
    git clone https://github.com/thaynnaraluciano/luizaLabsChallenge.git
    ```
* Acesse o diretório *luizaLabsChallenge*
    ```
    cd luizaLabsChallenge
    ```
* Abra a solution *NotificationApi.sln* (disponível em: *luizaLabsChallenge/NotificationApi*) com o Visual Studio e preencha as credenciais do servidor SMTP no arquivo *appsettings.json*, localizado na camada de *Presentation*, no projeto *Api.csproj*. Para ambiente de desenvolvimento, sugiro a utilização da ferramenta [mailTrap](https://mailtrap.io/). O mailTrap é um serviço para teste de envio de e-mails e a próxima seção contém as instruções para sua utilização.
* Execute o comando abaixo para executar os containers do projeto: 
    ```
    docker compose up -d --build
    ```
* Com os containers rodando, será possível acessar a aplicação pelo navegador. O endereço padrão definido é http://localhost:5173
* Para parar a execução da aplicação, execute o comando abaixo no mesmo diretório em que foi executado o comando para iniciar a execução:
    ```
    docker compose down
    ```

### Configurando o MailTrap
* Acesse o [MailTrap](https://mailtrap.io/)
* Crie uma conta na ferramenta e faça login
* Na tela inicial da ferramenta clique em "Start testing" na seção "EmailTesting"
* Serão exibidas na tela as credenciais do servidor SFTP. Estas são as informações que devem ser informadas no arquivo *appsettings.json*
* Nesta mesma tela, serão exibidos os emails que forem enviados durante os testes da aplicação

## Escolhas de desenvolvimento


A aplicação desenvolvida é um monorepo, para facilitar a execução em ambiente local utilizando o docker compose. A aplicação consiste em dois microsserviços: UserApi e NotificationApi, além do projeto de Frontend. Todos os projetos foram desevolvidos seguindo as boas práticas de programação e foram baseadas no DDD. As APIs seguiram a arquitetura em camadas.

A UserApi é o ponto de entrada do backend e é responsável por expor os endpoints de criação de usuário, login, confirmação de email e reenvio de email. 

A NotificationApi é um microsserviço responsável pelo envio de notificações. Inicialmente, ele está responsável pelo disparo de e-mails via Smtp, podendo ser evoluído para abranger outros tipos de notificação, como SMS, por exemplo.

O Frontend foi escrito em Vue.js e utiliza bibliotecas como Tailwind para estilização e Pinia para armazenar e gerenciar estados.

Os testes criados nas APIs foram unitários, utilizando as bibliotecas NUnit e Moq. Os testes foram focados em garantir o funcionamento das validações e o tratamento de exceções. No frontend não foram criados testes.

Para logs foi utilizada a interface ILogger da biblioteca Microsoft.Extensions.Logging. Utilizando-a é possível gerar logs de information, warning, error, entre outros e os logs podem ser consultados no ambiente onde for realizado o deploy, como por exemplo, Datadog. Na execução local com o docker compose, é possível ver os logs no terminal, caso o comando executado para subir os containers via docker compose não contenha a instrução *-d*.

O tratamento de exceções é realizado pela captura de exceções e tratamento conforme statusCodes do Http via exceptions personalizadas. A captura e retorno padronizado das exceções foi configurado via Middleware.

Por questões de segurança da aplicação, o usuário deve criar uma senha forte durante o cadastro. A definição de senha forte considerada foi ter no mínimo 8 dígitos, contendo caracteres maiúsculos, minúsculos, números e caracteres especiais. Estes requisitos estão sendo validados tanto no frontend quanto no backend.

Como adicional às funcionalidades que foram solicitadas no desafio, foi criada a funcionalidade de reenvio de e-mail de confirmação. Este item foi adicionado visando complementar o funcionamento básico da aplicação solicitada. Considerando cenários em que o usuário foi salvo no banco, mas por algum motivo não conseguiu validar seu e-mail, ele poderá solicitar o reenvio e confirmar sua conta a qualquer momento.
