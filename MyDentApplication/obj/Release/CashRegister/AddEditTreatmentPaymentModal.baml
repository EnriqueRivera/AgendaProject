   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   MyDentApplication4   .MyDentApplication.AddEditTreatmentPaymentModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$  Agregar tratamiento a caja��$��444.5��$	��279�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ lblTotalAmount��$��
Descuento:��$
��Left=�$��30��$��27.423,85.126,0,0q�$	��Top=�$��92.334��$��16(��� -   $ lblBank��$��Tratamiento:��$
��Left=�$��30��$��17.76,15.126,0,0q�$	��Top=�$��101.997��$��16(��� -   $ lblVoucherCheckNumber��$��Costo: $��$
��Left=�$��30��$��53.267,50.126,0,0q�$	��Top=�$��66.49��$��16(��� -    ��Name$ txtTreatmentCost��$
��Left=�$��30��$��124.757,51.126,0,0q�$	��Top=�$��223.076��$��2��$��1q�
���   �$��16(��� -    ��Name$ btnAddTreatment��$��Agregar��$
��Left=�$��116.602,0,0,16q�$��91��	 � Cursor$
	 Hand��$��7��$��29��$��Bottom=��� -   $ 	btnCancel��$��Cancelar��$
��Left=�$��225.602,0,0,16q�$��91��$
	 Hand��$��8��$��29��$��Bottom=��� -	   
 ��Name$
 
cbDiscount��$
��Left=�$��30��$��124.757,86.126,0,0q�$	��Top=�$��223.076��$��Right=�$��Center=�$��3��$��16(�.@���0����5����10����15����20����25����30���� -
   $ lblTotalAmount_Copy��$��%��$
��Left=�$��27��$��352.833,86.126,0,0q�$	��Top=�$��25.167��$��16(��� -   $  lblVoucherCheckNumber_Copy��$��	Cantidad:��$
��Left=�$��30��$��41.767,120.126,0,0q�$	��Top=�$��77.99��$��16(��� -   $ txtQuantity��$
��Left=�$��30��$��124.757,121.126,0,0q�$	��Top=�$��223.076��$��4��$��1q�
���   �$��16(��� -   $! lblVoucherCheckNumber_Copy1��$��Total: $��$
��Left=�$��30��$��58.267,155.126,0,0q�$	��Top=�$��61.49��$��16(��� -   $ txtTotalAmount��$
��Left=�$��30��$��124.757,156.126,0,0q�$	��Top=�$��223.076��$��5��$��1q�
���   �{�.  $��16(��� -   $ 
btnRefresh��$��301.59,143.126,0,0q�$
	 Hand��$
��Left=�$��25��$��25��$	��Top=�$��6��$y�Hidden=�.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20���� -   $ btnFindTreatment��$��0,16.126,46,0q�$
	 Hand��$��Right=�$��30��$��30��$	��Top=�$��6�� � ToolTip$ Buscar tratamiento...��.���� $A��;/MyDentApplication;component/Images/find_treatment_icon.png��$
��Fill=�$��20���� -   $ txtTreatment��$
��Left=�$��30��$��124.757,16.126,0,0q�$	��Top=�$��223.076��$��2��$��1q�
���   �$��16(��� -   $ btnRemoveTreatment��$��0,16.126,10,0q�$
	 Hand��$��30��$	��Top=�$��6��$��Right=�$��30��$&  Remover tratamiento seleccionado��.���� $9��3/MyDentApplication;component/Images/remove_icon.png��$
��Fill=�$��20��