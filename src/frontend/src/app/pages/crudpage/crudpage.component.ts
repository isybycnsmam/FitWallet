import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'crudpage',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './crudpage.component.html',
  styleUrl: './crudpage.component.scss'
})
export class CrudPageComponent {}
