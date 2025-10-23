import { Injectable, signal } from '@angular/core';

export interface BreadcrumbModel {
  title: string;
  url: string;
  icon: string;
  isActive?: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class BreadcrumbService {
  readonly data = signal<BreadcrumbModel[]>([]);

  reset() {
    const dashboard: BreadcrumbModel = {
      title: 'Dashboard',
      url: '/',
      icon: 'bi-speedometer2',
    };

    this.data.set([{ ...dashboard }]);
  }

  setDashboard() {
    const dashboard: BreadcrumbModel = {
      title: 'Dashboard',
      url: '/',
      icon: 'bi-speedometer2',
      isActive: true,
    };

    this.data.set([{ ...dashboard }]);
  }

  set(breadcrumbs: BreadcrumbModel[]) {
    this.data.update((prev) => [...prev, ...breadcrumbs]);
  }
}
