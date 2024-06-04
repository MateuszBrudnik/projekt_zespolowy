import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(private authService: AuthService, private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const { email, password, confirmPassword } = this.registerForm.value;
      this.authService.register(email, password, confirmPassword).subscribe({
        next: (response) => {
          this.successMessage = response.message;
          this.errorMessage = null;
        },
        error: (err) => {
          console.error(err);
          this.errorMessage = err.error?.['']?.[0] || 'Registration failed. Please try again.';
          this.successMessage = null;
        }
      });
    }
  }
}
