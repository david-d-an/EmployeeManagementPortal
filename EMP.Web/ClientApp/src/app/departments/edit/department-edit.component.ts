import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DepartmentService } from '../service/department.service';

@Component({
  selector: 'app-department-edit',
  templateUrl: './department-edit.component.html',
  styleUrls: ['./department-edit.component.css']
})
export class DepartmentEditComponent implements OnInit {

  constructor(
    private productService: DepartmentService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
  }

}
