   M S B A M L   `   `   `  ����   MyDentApplication4    .MyDentApplication.CleanedMaterialsDetailsModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window�� � Title$ Detalles de la limpieza��$	��413��$��481.3�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ btnClose��$��Cerrar��$��	0,0,10,10q� � Cursor$
 Hand��$��8��$��29��$��Bottom=�$��Right=�$��91���� $��Visible=�$��Disabled=�$��10,10,10,44q�.���� $��Center=�$��Center=� �� Orientation$ 
Horizontal=�.+��� -   	 ��Name$	 gbGeneralInfo��$��Información general��$
��Bold"�$��2q�
���   �
���   �$��372.999��$��357.5��.��� -   $ gridGeneralInfo��$��0,10,-2,2.245q�.+��� $��Fecha:��$
��Left=�$��30��$��	55,10,0,0q�$	��Top=�$��48.667��$��Normal"��� $��Grupo:��$
��Left=�$��30��$��	55,45,0,0q�$	��Top=�$��48.667��$��Normal"��� $��Observaciones:��$
��Left=�$��30��$��	10,80,0,0q�$	��Top=�$��93.667��$��Normal"��� $��1q�
���   �$��10,110,10,0q�$��118.835��$	��Top=�._��� -   
 ��Name$ 
 txtGeneralInfoObservations��$��0,0,0,0q�$��Italic$�" �� VerticalScrollBarVisibility$
 Auto=� �� TextWrapping$
 Wrap=� �� 
IsReadOnly . $��Normal"��� $��Lavado:��$
��Left=�$��30��$��47.333,233.835,0,0q�$	��Top=�$��56.334��$��Normal"��� $��Empaquetado:��$
��Left=�$��30��$��10,268.835,0,0q�$	��Top=�$��93.667��$��Normal"��� $��Esterilizado:��$
��Left=�$��30��$��22,303.835,0,0q�$	��Top=�$��81.667��$��Normal"��� -    ��Name$ 
chkCleaned��$
��Left=�$��15��$��108.667,240.835,0,0q�$	��Top=�$
��13.5��{�.  �� -   $ chkPackaging��$
��Left=�$��15��$��108.667,276.166,0,0q�$	��Top=�$
��13.5��{�.  �� -	   $ chkSterilized��$
��Left=�$��15��$��108.667,311.166,0,0q�$	��Top=�$
��13.5��{�.  �� -
    ��Name$ txtGeneralInfoDate��$�� ��$
��Left=�$��30��$��
115,10,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� -   $ txtGroupLetter��$�� ��$
��Left=�$��30��$��
115,45,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� -   $	 gbClean��$��Lavado��$
��Bold"�$��2q�
���   �
���   �$��372.999��$��357.5��$y�	Collapsed=�.��� -   $ 	gridClean��$��0,10,-2,2.245q�.+��� $��Fecha:��$
��Left=�$��30��$��55,38.5,0,0q�$	��Top=�$��48.667��$��Normal"��� $��Usuario:��$
��Left=�$��30��$��47.333,108.5,0,0q�$	��Top=�$��56.334��$��Normal"��� $��Observaciones:��$
��Left=�$��30��$��10,143.5,0,0q�$	��Top=�$��93.667��$��Normal"��� $��1q�
���   �$��10,173.5,10,0q�$��118.835��$	��Top=�._��� -   $
 txtCleanObservations��$��0,0,0,0q�$��Italic$�$
 Auto=�$
 Wrap=� . $��Normal"��� -   $ txtCleanDate��$�� ��$
��Left=�$��30��$��115,38.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� -   $ txtCleanShift��$�� ��$
��Left=�$��30��$��115,73.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� $��Turno:��$
��Left=�$��30��$��55,73.5,0,0q�$	��Top=�$��48.667��$��Normal"��� -   $ txtCleanUser��$�� ��$
��Left=�$��30��$��115,108.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� -   $	 
gbPackaged��$��Empaquetado��$
��Bold"�$��2q�
���   �
���   �$��372.999��$��357.5��$y�	Collapsed=�.��� -   $ 
gridClean1��$��0,10,-2,2.245q�.+��� $��Fecha:��$
��Left=�$��30��$��55,38.5,0,0q�$	��Top=�$��48.667��$��Normal"��� $��Usuario:��$
��Left=�$��30��$��47.333,108.5,0,0q�$	��Top=�$��56.334��$��Normal"��� $��Observaciones:��$
��Left=�$��30��$��10,143.5,0,0q�$	��Top=�$��93.667��$��Normal"��� $��1q�
���   �$��10,173.5,10,0q�$��118.835��$	��Top=�._��� -   $
 txtPackagedObservations��$��0,0,0,0q�$��Italic$�$
 Auto=�$
 Wrap=� . $��Normal"��� -   $ txtPackagedDate��$�� ��$
��Left=�$��30��$��115,38.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� -   $ txtPackagedShift��$�� ��$
��Left=�$��30��$��115,73.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� $��Turno:��$
��Left=�$��30��$��55,73.5,0,0q�$	��Top=�$��48.667��$��Normal"��� -   $ txtPackagedUser��$�� ��$
��Left=�$��30��$��115,108.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� -   $	 gbSterilized��$��Esterilizado��$
��Bold"�$��2q�
���   �
���   �$��372.999��$��357.5��$y�	Collapsed=�.��� -   $ 
gridClean2��$��0,10,-2,2.245q�.+��� $��Fecha:��$
��Left=�$��30��$��55,38.5,0,0q�$	��Top=�$��48.667��$��Normal"��� $��Usuario:��$
��Left=�$��30��$��47.333,108.5,0,0q�$	��Top=�$��56.334��$��Normal"��� $��Observaciones:��$
��Left=�$��30��$��10,143.5,0,0q�$	��Top=�$��93.667��$��Normal"��� $��1q�
���   �$��10,173.5,10,0q�$��118.835��$	��Top=�._��� -   $
 txtSterilizedObservations��$��0,0,0,0q�$��Italic$�$
 Auto=�$
 Wrap=� . $��Normal"��� -   $ txtSterilizedDate��$�� ��$
��Left=�$��30��$��115,38.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� -   $ txtSterilizedShift��$�� ��$
��Left=�$��30��$��115,73.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$��� $��Turno:��$
��Left=�$��30��$��55,73.5,0,0q�$	��Top=�$��48.667��$��Normal"��� -   $ txtSterilizedUser��$�� ��$
��Left=�$��30��$��115,108.5,0,0q�$	��Top=�$��222.5��$��Normal"�$��Italic$�