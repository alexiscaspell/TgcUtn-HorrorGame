using System;
using Microsoft.DirectX;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    internal interface Comportamiento
    {
        Vector3 proximoPunto(Vector3 posicionActual);
    }
}