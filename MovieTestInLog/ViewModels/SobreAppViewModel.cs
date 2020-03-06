namespace MovieTestInLog.ViewModels
{
    public class SobreAppViewModel : BaseViewModel
    {
        private string _sobreoapp;
        public string Sobreoapp
        {
            get { return _sobreoapp; }
            set { SetProperty(ref _sobreoapp, value); }
        }

        public SobreAppViewModel()
        {
            Sobreoapp = "Este aplicativo foi desenvolvido com a finalidade de realizar um teste para uma vaga de programador Xamarin na empresa InLog.\n" +
                "Usando:\n" +
                 "Xamarin Forms para iOS, Android e UWP\n" +
                "MVVM\n" +
                "Infinity Scroll com paginação\n" +
                "Função para procurar filmes utilizando o sistema de buscas da API\n" +
                "Cache de imagem\n" +
                "Um projeto simples consumindo a API The Movie Database (TMDb)\n" +
                "\n" +
                "Espero ter atingido as espectativas... Obrigado!";
        }
    }
}
