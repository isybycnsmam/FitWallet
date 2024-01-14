import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'adminpage',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './adminpage.component.html',
  styleUrl: './adminpage.component.scss'
})
export class AdminPageComponent {}
