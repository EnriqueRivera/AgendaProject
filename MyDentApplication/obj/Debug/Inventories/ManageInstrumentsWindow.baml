   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 6g   /    )MyDentApplication.ManageInstrumentsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  60   3System&clr-namespace:System;assembly=mscorlib  6g   -   5          �Name$  Window��5       � Title$  Inventario de instrumental��5      $��782.716��5      $	��480��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6W   .��5	      � -   5         �Name$ 
LayoutRoot��5	      .+�5
      &   System.Windows.Controls.DataGrid  -   5          Name$ dgInstruments��5
      $��3��6$      SelectionMode$ Single=�61   $��10,46.5,10,103q�6H      
IsReadOnly .    CanUserAddRows .  	   CanUserSortColumns	 . 
   CanUserDeleteRows
 .     AutoGenerateColumns .  ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Instrumento��6<      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.07* 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *InstrumentName+6Q     5      $ Cantidad actual��66   $ 0.07* 6O    6   �� *
Quantity+6O     5      $ Usado en��6;   $ 0.07* 6M    6   �� *TreatmentName+6M     5      $ Usos restantes��66   $ 0.07* 6N    6   �� *
UsesLeft+6N     5      $ 
Comentario��65   $ 0.07* 6I    6   �� *	Comment+6I   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Revisado��6;   $ 0.03* 6M    6   �� *	IsChecked+6M   5      �� 5      -   5         ��Name$ btnDeleteInstrument��5      $	��130��6(   $��Bottom=�64   $��6��6O   $��	0,0,10,69q�6\   $��29��6o   $��Right=�6{   $��Eliminar instrumento��6�    � Cursor$
 Hand��6�   6�   �� 5      -   5        $ btnEditInstrument��5      $	��119��6&   $��Bottom=�62   $��5��6M   $��
0,0,145,69q�6Z   $��29��6n   $��Right=�6z   $��Editar instrumento��6�   $
 Hand��6�   6�   �� 5      -   5        $ btnAddInstrument��5      $	��144��6%   $��Bottom=�61   $��4��6L   $��
0,0,269,69q�6Y   $��29��6m   $��Right=�6y   $��Agregar instrumento��6�   $
 Hand��6�   6�   �� 5      $	��118��6
   $	��Top=�6   $��
10,8.5,0,0q�6.   $��27��6B   $
��Left=�6N   $��Seleccione un mes:��6i   &   Xceed.Wpf.Toolkit.DateTimeUpDown  5      -   5          Name$ dtudSelectedMonth��5      $
��Left=�63   $
��25.5��6N   $��
133,10,0,0q�6\   $	��Top=�6p   $��187.5��6�      Format$ 	YearMonth=�6�    K� ZIndex$ 1��6�   �� 5      -   5        $ 
btnRefresh��5      $��325.5,10,0,0q�6   $
 Hand��65   $
��Left=�6C   $��25��6^   $��25��6i   $	��Top=�6u   .��5      �� $:��4/MyDentApplication;component/Images/refresh_icon.png��6   $
��Fill=�6I   $��20��6X   5      �� 5      -	   5        $ btnSignature��5      $��158.216��6!   $��Bottom=�61   $��4��6L   $��	10,0,0,69q�6Y   $��29��6l   $
��Left=�6x   $��Firmar inventario del mes��6�   $
 Hand��6�   6�   �� 5      $��113.5��6
   $	��Top=�6   $��
396,10,0,0q�60   $��27��6D   $
��Left=�6P   $��Fecha seleccionada:��6k   �� 5       -
   5         ��Name$ lblSelectedMonth��5    
   $	��Top=�6$   $��514.5,10,10,0q�6<   $��27��6S   $�� ��6_   �� 5!      -   5        $ btnAddEditRevision��5!      $��182.5��6'   $��Bottom=�65   $��5��6P   $��0,0,10,27.47q�6]   $��29��6s   $��Right=�6   $��Agregar/Editar revisión��6�   $
 Hand��6�   6�   �� 5"      $
��53.5��6
   $��Bottom=�6   $��	10,0,0,37q�62   $��27��6E   $
��Left=�6Q   $��Firma 1:��6l   �� 5#      $
��53.5��6
   $��	10,0,0,10q�6   $
��Left=�6*   $��Firma 2:��6E   $��27��6X   $��Bottom=�6d   �� 5$      -   5        $ lblSignature��5$   
   $��Bottom=�6    $��68.5,0,424.216,37q�6;   $��27��6V   $�� ��6b   �� 5%      -   5        $ lblSignature1��5%   
   $��68.5,0,424.216,10q�6!   $�� ��6<   $��27��6G   $��Bottom=�6S   5&      5'      