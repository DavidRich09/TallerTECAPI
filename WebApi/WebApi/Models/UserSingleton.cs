namespace WebApi.Models;

/**
 * Clase singleton que representa al usuario activo en la aplicación.
 */
public class UserSingleton
{
    private static UserSingleton instance = null;

    public string id;
    /**
     * Constructor privado para evitar instanciación.
     */
    private UserSingleton()
    {
        
    }
/**
 * Método que devuelve la instancia de la clase.
 */
    public static UserSingleton GetInstance()
    {
        if (instance == null)
        {
            instance = new UserSingleton();
        }
        return instance;
    }
}