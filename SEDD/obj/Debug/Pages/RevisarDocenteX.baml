   M S B A M L   `   `   `  ����?  ;SEDD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SEDDSEDD  5           SEDD.Pages.RevisarDocenteX   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      localclr-namespace:SEDD  5        N� Title$  Page1��5
       � 	Resources 5       �� MergedDictionaries/  )MaterialDesignThemes.Wpf.CustomColorTheme   	BaseTheme   PrimaryColor   SecondaryColor �� Source�� 5   
    5        5      $ LightX�62   $ #6F7271X�6D   $ 	DarkGreenX�6[   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml?�6%   %    5      .��5      � -   5        $
��grid��5      
�������$	��733��6*   $
��1346��67   .+�5   
   �� -   5        $
��rect��5   �   ���#1B396A$��Center=�6#   $��45��6@   
���   �$	��Top=�6[   $
��1346��6s   $��0,0,0,0q�6�   �� 5   
   -   5         ��Name$ departamentoLabel��5      $�� ��6+   $
��Left=�66   $��5,10,0,0q�6Q   $	��Top=�6c   �� 5   
   -   5         ��Name$ menubtn��5      $��Menú revisor��6"   $
��Left=�69   $��	470,5,0,0q�6T   $	��Top=�6g   $	��164��6   ���#1B396A���#0000006�   �� 5   
   -   5        $ listabtn��5      $��Lista de docentes��6#   $
��Left=�6?   $��	670,5,0,0q�6Z   $	��Top=�6m   $	��164��6�   ���#1B396A���#0000006�   �� 5   
   -   5        $��Cerrar sesión��5      $
��Left=�6)   $��1179,60,0,0q�6D   $	��Top=�6Y   $	��134��6q   6}   �� 5   
   -   5        $��Atrás��5      $
��Left=�6!   $��	50,60,0,0q�6<   $	��Top=�6O   $	��134��6g   6s   �� 5   
   -   5        $ at��5      $��Anterior��6   $
��Left=�60   $��
510,60,0,0q�6K   $	��Top=�6_   $	��134��6w   6�   �� 5   
   -	   5        $	 sig��5      $��	Siguiente��6   $
��Left=�62   $��
660,60,0,0q�6M   $	��Top=�6a   $	��134��6y   6�   �� 5    
   -
   5        $ punto��5       $��Label��6   $
��Left=�6/   $��974,116,0,0q�6J   $	��Top=�6_   $��21(�6w   $
��Bold"�6�   �� 5"   
   $
��Left=�6   $��177,116,0,0q�6+   $!��DOCUMENTOS - REQUISITOS DE:��6@   $	��Top=�6f   $��70��6~   $	��896��6�   $��
Montserrat)�6�   $��34(�6�   ���#1B396A$
��Bold"�6�   �� 5#   
   ���	#FFF4F4F5$
��Left=�6%   $��1��6@   $��148,165,0,0q�6K   
���   �$	��Top=�6o   $
��1007��6�   �� 5%   
   -   5        	 ��Name$	 stack��5%      $��Center=�6$   $	��484��6A   $��177,190,0,0q�6N   $	��Top=�6c   $	��994��6{   
 �� Orientation$
 Vertical=�6�   .+�5&      �� -   5        $ 	Nombretxt��5&      $��Nombre(s) Apellidos��6'   $
��Left=�6E   $��0,0,0,0q�6`   $	��Top=�6q   $��61��6�   $	��896��6�   $��
Montserrat)�6�   $��48(�6�   �� 5'      ��. .��5(      �� -   5        $	 
stackChild��5(      $	��315��61   $��0,70,0,0q�6>   $	��Top=�6P   $	��963��6h   5)      5+      �� 5-   
   $
��Left=�6   $��45��6+   $	��Top=�67   $	��102��6O   $��
1179,0,0,0q�6[   $-��'/SEDD;component/Imagenes/tecNM_logo.jpg��6o   �� 5.   
   $
��Left=�6   $��45��6+   $	��Top=�67   $��50��6O   $��
1286,0,0,0q�6Z   $/��)/SEDD;component/Imagenes/logo-itcm-v2.png��6n    � 5/   
   -   5          �Name$ RFrame��5/        � NavigationUIVisibility$ Hidden=�6    52      53      