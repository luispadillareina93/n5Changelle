import React from 'react';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Link } from 'react-router-dom';

import Logo from '../assets/n5-logo.png'
import EcuadorFlag from '../assets/ecuador-flag.png'

function Header() {
  return (
    <AppBar position="static">
      <Toolbar>
      <Link to="/">
        <img src={Logo} alt="N5 logo" style={{ width: 70, marginRight: 16 }} />
        </Link>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
        
          Permission-App
        </Typography>
        <IconButton color="inherit">
        <img src={EcuadorFlag} alt="flag-icon" style={{ width: 50, marginRight: 8 }} />
          <Typography variant="subtitle1" style={{ marginLeft: 8 }}>
            Luis Padilla
          </Typography>
          <AccountCircleIcon style={{ marginLeft: 8 }} />
        </IconButton>
      </Toolbar>
    </AppBar>
  );
}

export default Header;
