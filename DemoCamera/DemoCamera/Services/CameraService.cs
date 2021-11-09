using System;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace DemoCamera.Services
{
    class CameraService
    {
        PermissionStatus cameraOK;
        PermissionStatus storageOK;

        public async Task Init()
        {
            await CrossMedia.Current.Initialize();
            // Pedir permisos de acceso a camara y al almacenamiento del dispositivo
            cameraOK = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            storageOK = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraOK != PermissionStatus.Granted || storageOK != PermissionStatus.Granted)
            {
                var status = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });

                cameraOK = status[Permission.Camera];
                storageOK = status[Permission.Storage];
            }
        }
        /**
         * Hacer una foto con la camara
         * 
         * 
         */
        public async Task<MediaFile> TakePhoto()
        {
            if (cameraOK == PermissionStatus.Granted
                && storageOK == PermissionStatus.Granted
                && CrossMedia.Current.IsCameraAvailable
                && CrossMedia.Current.IsTakePhotoSupported)
            {
                var options = new StoreCameraMediaOptions
                {
                    DefaultCamera = CameraDevice.Front, // Doesn't always work on Android, depends on Device
                   // AllowCropping = true, // UWP & iOS only,
                    PhotoSize = PhotoSize.Medium, // if Custom, you can set CustomPhotoSize = percentage_value 
                    CompressionQuality = 90,
                    Directory = "DemoCamara",
                    Name = $"{Guid.NewGuid()}.jpg",
                    SaveToAlbum = true
                };

                var file = await CrossMedia.Current.TakePhotoAsync(options);
                return file;
            }

            return null;
        }

        /**
         * Seleccionar una foto
         * 
         */
        public async Task<MediaFile> ChoosePhoto()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var file = await CrossMedia.Current.PickPhotoAsync();
                return file;
            }

            return null;
        }

        public async Task<MediaFile> TakeVideo()
        {
            if (cameraOK == PermissionStatus.Granted
                && storageOK == PermissionStatus.Granted
                && CrossMedia.Current.IsTakeVideoSupported)
            {
                var options = new StoreVideoOptions { SaveToAlbum = true };
                var file = await CrossMedia.Current.TakeVideoAsync(options);
                return file;
            }

            return null;
        }

        /**
         * 
         * Seleccionar un video
         * 
         * 
         */
        public async Task<MediaFile> ChooseVideo()
        {
            if (CrossMedia.Current.IsPickVideoSupported)
            {
                var file = await CrossMedia.Current.PickVideoAsync();
                return file;
            }

            return null;
        }
    }
}
