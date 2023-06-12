namespace CinemaManager.Models
{
    public static class MessagesModel
    {
        public const string MovieSuccesfulyCreated = "Filme criado com sucesso!";

        public const string MovieAlreadyRegistered = "Filme já cadastrado na base de dados.";

        public const string UnableToCreateMovie = "Ocorreu um erro ao criar o filme. Tente novamente e se o problema persistir contate o admnistrador.";

        public const string MovieSuccesfulyUpdated = "Filme atualizado com sucesso!";

        public const string UnableToUpdateMovie = "Ocorreu um erro ao atualizar o filme. Tente novamente e se o problema persistir contate o admnistrador.";

        public const string MovieSuccesfulyRemoved = "Filme removido com sucesso!";

        public const string MovieLinkedToASession = "Não foi possível remover o filme, pois o mesmo está vinculado a uma sessão.";

        public const string UnableToRemoveMovie = "Ocorreu um erro ao remover o filme. Tente novamente e se o problema persistir contate o admnistrador.";

        public const string InvalidSessionStartDate = "A data do horário do início da sessão não pode ser menor que a data da sessão.";

        public const string MovieNotFound = "Filme não encontrado.";

        public const string TheaterNotFound = "Sala não encontrada.";

        public const string SessionAlreadyAllocated = "Sessão com horário já alocado para outro filme.";

        public const string SessionSuccesfulyCreated = "Sessão criada com sucesso!";

        public const string UnableToCreateSession = "Ocorreu um erro ao criar a sessão. Tente novamente e se o problema persistir contate o admnistrador.";

        public const string SessionSuccesfulyRemoved = "Sessão removida com sucesso!";

        public const string SessionCannotBeRemoved = "Sessão não pode ser removida, pois faltam 10 dias ou menos para sua exibição.";

        public const string UnableToRemoveSession = "Ocorreu um erro ao remover a sessão. Tente novamente e se o problema persistir contate o admnistrador.";

        public const string UserSuccesfulyCreated = "Usuário criado com sucesso!";

        public const string EmailOrLoginAlreadyRegistered = "E-mail e/ou Login já cadastrado na base de dados.";

        public const string UnableToCreateUser = "Ocorreu um erro ao criar o usuário. Tente novamente e se o problema persistir contate o admnistrador.";

        public const string UserSuccesfulyUpdated = "Usuário atualizado com sucesso!";

        public const string UnableToUpdateUser = "Ocorreu um erro ao atualizar o usuário. Tente novamente e se o problema persistir contate o admnistrador.";

        public const string UserSuccesfulyRemoved = "Usuário removido com sucesso!";

        public const string UnableToRemoveUser = "Ocorreu um erro ao remover o usuário. Tente novamente e se o problema persistir contate o admnistrador.";

        public const string InvalidCredentials = "Login e/ou senha inválidos.";

        public const string UnableToSignIn = "Ocorreu um erro ao tentar realizar o login. Tente novamente e se o problema persistir contate o admnistrador.";
    }
}