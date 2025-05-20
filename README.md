# Result Pattern - C#

Este projeto implementa o **Result Pattern** de forma simples, clara e agnóstica a protocolos de transporte.
O objetivo é **padronizar o retorno de operações da aplicação e domínio**, evitando o uso de exceções para controle de fluxo e facilitando a comunicação entre camadas.

---

## ✅ Benefícios

- Forte adesão a princípios de **Clean Architecture**.
- Separação clara entre **sucesso** e **falha** nas operações.
- Imutabilidade dos resultados.
- Representação rica e extensível de erros.
- Facilita testes unitários e integração.

---

## 📚 Componentes Principais

### `ResultType`

Define os possíveis estados de um resultado:

- `Success` — Operação concluída com sucesso.
- `Invalid` — Dados de entrada inválidos (erros de validação).
- `Conflict` — Conflito de estado (exemplo: recurso em uso).
- `NotFound` — Recurso não encontrado.
- `RuleViolation` — Violação de regra de negócio ou domínio.
- `Error` — Erro inesperado ou falha técnica.

---

### `Error`

Representa um erro associado a uma operação.

| Propriedade | Descrição            |
|-------------|----------------------|
| `Code`      | Código do erro.      |
| `Message`   | Mensagem descritiva. |
| `Target`    | (Opcional) Campo ou entidade relacionada ao erro. |

Exemplo de criação:

```csharp
var error = Error.Create("INVALID_EMAIL", "O e-mail informado é inválido.", "Email");
```

---

### `Result`

Representa o resultado de uma operação sem valor de retorno.

#### 🔹 Com um único erro

```csharp
var result = Result.FromInvalid(
    Error.Create("NAME_REQUIRED", "O nome é obrigatório.", "Name")
);
```

#### 🔹 Com múltiplos erros

```csharp
var result = Result.FromInvalid(new[]
{
    Error.Create("NAME_REQUIRED", "O nome é obrigatório.", "Name"),
    Error.Create("EMAIL_INVALID", "O e-mail informado é inválido.", "Email")
});
```

Verificação de estado:

```csharp
if (result.IsFailure)
{
    // Tratar erros...
}
```

---

### `Result<T>`

Representa o resultado de uma operação com valor de retorno.

#### 🔹 Com múltiplos erros

```csharp
var result = Result<User>.FromInvalid(new[]
{
    Error.Create("EMAIL_IN_USE", "Este e-mail já está em uso.", "Email"),
    Error.Create("CPF_INVALID", "O CPF informado é inválido.", "Cpf")
});
```

#### 🔹 Com sucesso

```csharp
var user = new User("Jackson");
var result = Result<User>.WithSuccess(user);
```

---

## 📌 Convenções de Uso

| Cenário                  | ResultType  | Exemplo               |
|--------------------------|-------------|-----------------------|
| Validação de dados       | `Invalid`   | Campos inválidos.    |
| Recurso não encontrado   | `NotFound`  | ID não existe.       |
| Conflito de estado       | `Conflict`  | E-mail já cadastrado.|
| Regra de negócio violada | `RuleViolation` | Status inválido. |
| Erro técnico inesperado  | `Error`     | Falha de banco, etc. |
| Operação bem-sucedida    | `Success`   | Tudo certo!          |

---

## 🛠️ Exemplo de Uso no Domínio

```csharp
public Result Activate(DateTime utcNow)
{
    if (!IsPendingActivation)
    {
        return Result.FromRuleViolation(
            Error.Create("STATUS_INVALID",
                "Somente usuários pendentes podem ser ativados.",
                "Status"));
    }

    Status = UserStatus.Active;
    ActivatedOn = utcNow;

    return Result.WithSuccess();
}
```

---

## 📖 Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests para melhorias.

---

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).