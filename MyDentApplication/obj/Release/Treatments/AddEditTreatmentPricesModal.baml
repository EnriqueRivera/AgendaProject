   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   MyDentApplication3   -MyDentApplication.AddEditTreatmentPricesModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$! Agregar tratamiento y costo��$	��426��$	��331�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=�.��� -    �Name$ 
LayoutRoot��$��	0,0,-2,-2q�.+��� -    ��Name$ btnAddUpdateTreatment��$��Agregar��$��110.17,0,0,10q� � Cursor$
 Hand��$��5��$��29��$��Bottom=�$
��Left=�$��91���� -   $ 	btnCancel��$��Cancelar��$��206.17,0,0,10q�$
 Hand��$��6��$��29��$��Bottom=�$
��Left=�$��91���� -    ��Name$ lblTreatmentName��$��Nombre del tratamiento:��$
��Left=�$��27��$��	10,10,0,0q�$	��Top=�$	��344���� -   	 ��Name$	 txtTreatmentName��$��27��$��
10,37,10,0q�
 �� TextWrapping$

 Wrap=�$	��Top=�$��1��$��1q�
���   � �� TextAlignment$ Center=��� -   $ lblTreatmentDuration��$��
Descuento:��$
��Left=�$��27��$��
15,178,0,0q�$	��Top=�$��71.333��$y�Hidden=��� -    ��Name$ 
cbDiscount��$
��Left=�$��28��$��91.333,177,0,0q�$	��Top=�$	��100�� �� SelectedIndex$ 0��$��Right=�$��Center=�$��4��$y�Hidden=�.@���0����5����10����15����20����25����30���� -	   $ lblPercentCaption��$��%��$
��Left=�$��27��$��191.333,178,0,0q�$	��Top=�$	��116��$y�Hidden=��� -
   $ lblRecurrentDays��$��Costo: $��$
��Left=�$��27��$��37.833,128,0,0q�$	��Top=�$
��53.5���� -   $	 txtCost��$
��Left=�$��27��$��91.333,128,0,0q�$

 Wrap=�$	��Top=�$��96.837��$��3��$��1q�
���   �$��Center=��� -   $ lblTreatmentKey��$��Clave:��$
��Left=�$��27��$��46.833,82,0,0q�$	��Top=�$
��44.5���� -   $	 txtTreatmentKey��$
��Left=�$��27��$��91.333,82,0,0q�$

 Wrap=�$	��Top=�$��96.837��$��2��$��1q�
���   �$��Center=��� $8��2*Este tratamiento será agregado al año en curso.��$
��Left=�$��27��$��	15,0,0,50q�$��Bottom=�$	��355��$��Italic$�