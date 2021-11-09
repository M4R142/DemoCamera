using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

using DemoCamera.Services;

namespace DemoCamera.ViewModels
{
    class PhotoViewModel : BaseViewModel
    {
        CameraService cameraService;
        // Cargar imagenes desde fichero
        private ImageSource _photo;

        public ImageSource Photo
        {
            get { return _photo; }
            set { _photo = value; OnPropertyChanged(); }
        }

        public ICommand TakePhotoCommand { get; private set; }
        public ICommand ChoosePhotoCommand { get; private set; }

        public PhotoViewModel()
        {
            // Iniciar la camara
            cameraService = new CameraService();
            Task.Run(async () => await cameraService.Init());
            // Sacar foto
            TakePhotoCommand = new Command(async () => await TakePhoto());
            // Elegir foto
            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
        }

        private async Task TakePhoto()
        {
            // Esperar a que la camara esté lista y sacar la foto
            var file = await cameraService.TakePhoto();
            // Almacenar la imagen
            if (file != null)
                Photo = ImageSource.FromStream(() => file.GetStream()); 
        }

        private async Task ChoosePhoto()
        {
            // Esperar a que el servicio de seleccion de imagenes esté listo
            var file = await cameraService.ChoosePhoto();
            // Obtener la imagen
            if (file != null)
                Photo = ImageSource.FromStream(() => file.GetStream());
        }
    }
}
