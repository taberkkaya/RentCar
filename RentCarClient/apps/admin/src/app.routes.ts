import { Route } from '@angular/router';
import Layouts from './pages/layouts/layouts';

export const appRoutes: Route[] = [
  {
    path: '',
    loadComponent: () => import('./pages/layouts/layouts'),
    children: [
      { path: '', loadComponent: () => import('./pages/dashboard/dashboard') },
    ],
  },
];
