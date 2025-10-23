import { NgClass, Location, DatePipe } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  inject,
  input,
  ViewEncapsulation,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { EntityModel } from '../../models/entity.model';

@Component({
  selector: '<app-blank></app-blank>',
  imports: [NgClass, RouterLink, DatePipe],
  templateUrl: './blank.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class Blank {
  readonly pageIcon = input.required<string>();
  readonly pageTitle = input.required<string>();
  readonly pageDescription = input<string>('');
  readonly showStatus = input<boolean>(false);
  readonly status = input<boolean>(true);
  readonly showBackBtn = input<boolean>(true);
  readonly #location = inject(Location);
  readonly showEditBtn = input<boolean>(false);
  readonly editBtnUrl = input<string>('');

  readonly showAudit = input<boolean>(false);
  readonly audit = input<EntityModel>();

  back() {
    this.#location.back();
  }
}
