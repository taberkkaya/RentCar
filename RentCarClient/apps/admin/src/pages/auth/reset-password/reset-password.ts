import { NgClass } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  ElementRef,
  inject,
  signal,
  viewChild,
  ViewEncapsulation,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { HttpService } from 'apps/admin/src/services/http';
import { FlexiToastService } from 'flexi-toast';

@Component({
  imports: [FormsModule, NgClass, RouterLink],
  templateUrl: './reset-password.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ResetPassword {
  readonly id = signal<string>('');
  readonly loading = signal<boolean>(false);

  readonly #activated = inject(ActivatedRoute);
  readonly #toast = inject(FlexiToastService);
  readonly #http = inject(HttpService);
  readonly #router = inject(Router);

  readonly passwordRequirements = computed(() => {
    const pwd = this.password();
    return {
      minLength: pwd.length >= 8,
      hasUpperCase: /[A-Z]/.test(pwd),
      hasLowerCase: /[a-z]/.test(pwd),
      hasNumber: /[0-9]/.test(pwd),
      hasSpecialChar: /[!@#$%^&*(),.?":{}|<>]/.test(pwd),
    };
  });

  readonly passwordStrength = computed(() => {
    const requirements = this.passwordRequirements();
    const validCount = Object.values(requirements).filter(Boolean).length;
    if (validCount === 0) return { level: 0, text: 'Zayıf', class: '' };
    if (validCount <= 2)
      return { level: validCount, text: 'Zayıf', class: 'weak' };
    if (validCount <= 3)
      return { level: validCount, text: 'Orta', class: 'medium' };
    if (validCount <= 4)
      return { level: validCount, text: 'İyi', class: 'medium' };
    return { level: validCount, text: 'Güçlü', class: 'strong' };
  });

  readonly isPasswordValid = computed(() => {
    const requirements = this.passwordRequirements();
    return Object.values(requirements).every(Boolean);
  });

  readonly passwordsMatch = computed(() => {
    const pwd = this.password();
    const confirmPwd = this.confirmPassword();
    return pwd.length > 0 && confirmPwd.length > 0 && pwd === confirmPwd;
  });

  readonly isFormValid = computed(() => {
    return this.isPasswordValid() && this.passwordsMatch();
  });

  readonly strengthProgress = computed(() => {
    return (this.passwordStrength().level / 4) * 100;
  });

  readonly password = signal<string>('');
  readonly confirmPassword = signal<string>('');

  readonly newPasswordEl =
    viewChild<ElementRef<HTMLInputElement>>('newPasswordEl');
  readonly confirmPasswordEl =
    viewChild<ElementRef<HTMLInputElement>>('confirmPasswordEl');

  constructor() {
    this.#activated.params.subscribe((res) => {
      this.id.set(res['id']);
    });
  }

  toggleNewPassword() {
    this.newPasswordEl()?.nativeElement.type === 'password'
      ? this.newPasswordEl()?.nativeElement.setAttribute('type', 'text')
      : this.newPasswordEl()?.nativeElement.setAttribute('type', 'password');
  }

  toggleConfirmPassword() {
    this.confirmPasswordEl()?.nativeElement.type === 'password'
      ? this.confirmPasswordEl()?.nativeElement.setAttribute('type', 'text')
      : this.confirmPasswordEl()?.nativeElement.setAttribute(
          'type',
          'password'
        );
  }

  onSubmit(): void {
    if (this.isFormValid()) {
      const data = {
        forgotPasswordId: this.id(),
        newPassword: this.password(),
      };
      this.loading.set(true);
      this.#http.post<string>(
        '/rent/auth/reset-password',
        data,
        (res) => {
          this.#toast.showToast('Başarılı', res);
          this.#router.navigateByUrl('/login');
          this.loading.set(false);
        },
        () => {
          this.loading.set(false);
        }
      );
    } else {
      this.#toast.showToast(
        'Uyarı!',
        'Zorunlu alanları doldurmadınız.',
        'warning'
      );
    }
  }
}
