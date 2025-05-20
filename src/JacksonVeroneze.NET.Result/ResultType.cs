namespace JacksonVeroneze.NET.Result;

/// <summary>
/// Representa o resultado de uma operação de aplicação ou domínio.
/// Este enum é agnóstico ao protocolo de transporte (REST, gRPC, GraphQL, CLI).
///
/// A camada de apresentação deve mapear este resultado conforme o protocolo utilizado.
/// </summary>
public enum ResultType
{
    /// <summary>
    /// ✅ A operação foi concluída com sucesso.
    ///
    /// - REST:
    ///   - 200 OK (Operação com retorno de dados)
    ///   - 201 Created (Recurso criado, geralmente usado com POST)
    ///   - 204 No Content (Operação concluída, sem conteúdo de resposta)
    /// - gRPC: OK
    /// - GraphQL: Success
    /// - CLI: Exit Code 0
    ///
    /// Exemplo: Cadastro realizado, recurso atualizado, exclusão de recurso sem necessidade de resposta.
    /// </summary>
    Success,

    /// <summary>
    /// A entrada fornecida é inválida (erros de validação).
    ///
    /// - REST: 400 Bad Request
    /// - gRPC: INVALID_ARGUMENT
    /// - GraphQL: ValidationError
    /// - CLI: Exit Code 1
    ///
    /// Exemplo: Campos obrigatórios ausentes, formato de e-mail inválido, CPF inválido.
    /// </summary>
    Invalid,

    /// <summary>
    /// O estado atual do recurso impede a operação.
    ///
    /// - REST: 409 Conflict
    /// - gRPC: FAILED_PRECONDITION
    /// - GraphQL: ConflictError
    /// - CLI: Exit Code 2
    ///
    /// Exemplo: E-mail já em uso, tentativa de remoção de recurso com dependências.
    /// </summary>
    Conflict,

    /// <summary>
    /// ❌ O recurso ou entidade solicitada não foi encontrada.
    ///
    /// - REST: 404 Not Found
    /// - gRPC: NOT_FOUND
    /// - GraphQL: NotFoundError
    /// - CLI: Exit Code 3
    ///
    /// Exemplo: Busca ou atualização de registro inexistente.
    /// </summary>
    NotFound,

    /// <summary>
    /// A operação violou uma regra de negócio ou política de domínio.
    ///
    /// - REST: 422 Unprocessable Entity
    /// - gRPC: FAILED_PRECONDITION
    /// - GraphQL: RuleViolationError
    /// - CLI: Exit Code 4
    ///
    /// Exemplo: Tentativa de ativar um usuário que não está no status "pendente de ativação".
    /// </summary>
    RuleViolation,

    /// <summary>
    /// Ocorreu um erro técnico ou interno inesperado.
    ///
    /// - REST: 500 Internal Server Error
    /// - gRPC: INTERNAL
    /// - GraphQL: InternalError
    /// - CLI: Exit Code 99
    ///
    /// Exemplo: Falha de infraestrutura, exceção não tratada, erro de integração externa.
    /// </summary>
    Error
}