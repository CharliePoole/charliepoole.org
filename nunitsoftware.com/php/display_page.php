<?php

include( "build_menu.php" );

function drawNavButton( $name, $text="" )
{
   global $CurrentPageName;
    
   if ( $text == "" )
     $text = format_page_name( $name );
		
   if ( $name == activeButton() )
     echo "<a href=\"index.php?p=$name\" class=\"active\">$text</a>";
  else
     echo "<a href=\"index.php?p=$name\">$text</a>";
}

include( "template_funcs.php" );

function displayPage( $name, $html_dirs=NULL )
{
  global $CurrentPageName;
  global $CurrentPageDirectory;

  // Save name and release in globals
  $CurrentPageName = $name;

  // Initialize directory and type in globals
  $CurrentPageDirectory = HOME_DIR;
  if ( !is_array( $html_dirs ) )
     $html_dirs = array( HOME_DIR );

  // See if the site is offline
  if ( file_exists("OFFLINE") )
  	 getTemplate( "offlineTemplate.php" );
  // Make sure the page name is valid
  elseif ( !preg_match( "/^[a-z0-9\-]*$/i", $name ) )
     getTemplate( "notfoundTemplate.php" );
  // See whether the page exists and if so, where
  // it is and what type it is...
  else
  {
     $found = false;
   	 foreach( $html_dirs as $dir )
	 {
	    if ( file_exists( "$dir/$name.html" ) )
		{
		    $CurrentPageDirectory = "$dir/";
			$found = true;
			getTemplate( "pageTemplate.php" );
			break;
		}
	 }
	 
	 if ( !$found )
	    getTemplate( "notfoundTemplate.php" );
  }
}

?>
