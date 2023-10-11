import React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import Layout from './layout/Layout';
import Routes from './Routes';
import customTheme from './layout/customTheme';
import { ThemeProvider } from '@emotion/react';
function App() {
  return (
    <ThemeProvider theme={customTheme}>
      <Router>
        <Layout>
          <Routes />
        </Layout>
      </Router>
    </ThemeProvider>

  );
}

export default App;
