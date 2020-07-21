import { Component, OnInit, Inject } from '@angular/core';
import axios from 'axios';
import { ConversationList, Conversation, User } from './model/conversation.model';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'angular-chat';
  username = '';
  currentConversation: Conversation = {
    id: 0,
    partnerUserName: '',
    messages: []
  };
  newMessage = '';
  conversations: ConversationList[];
  userId: number;
  isLoading = false;
  conn: signalR.HubConnection;

  constructor(public dialog: MatDialog) {
    this.conn = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/chat')
      .build();
    this.conn.on('Chat', async (object: string) => {
      await this.getConversations();
      if (this.currentConversation.id) {
        await this.getConversation(this.currentConversation.id);
      }
    });
    this.conn.start().catch(err => {
      setTimeout(() => this.startSignalr(3000), 3000);
    });
  }

  startSignalr(retryInterval: number) {
    this.conn.start().catch(err => {
      setTimeout(() => this.startSignalr(retryInterval + 3000), retryInterval);
    });
  }

  async ngOnInit(): Promise<void> {
    this.isLoading = true;
    const savedUserId = localStorage.getItem('userId');
    if (savedUserId) {
      this.userId = +savedUserId;
      await this.getConversations();
    }
    this.isLoading = false;
  }

  async login() {
    const response = await axios.post(`http://localhost:5000/users/login/${this.username}`);
    this.userId = response.data.id;
    localStorage.setItem('userId', this.userId.toString());
    await this.getConversations();
  }

  openAddConversation() {
    const dialogRef = this.dialog.open(AddConversationComponent, {
      data: this.userId
    });
    dialogRef.afterClosed().subscribe(async () => {
      await this.getConversations();
    });
  }

  async getConversations() {
    const conversationsResponse = await axios.get(`http://localhost:5000/conversations?userId=${this.userId}`)
    this.conversations = conversationsResponse.data;
  }

  async getConversation(conversationId: number) {
    const response = await axios.get(`http://localhost:5000/conversations/${conversationId}?userId=${this.userId}`);
    this.currentConversation = response.data;
  }

  async sendMessage() {
    if (this.newMessage.trim() === '') {
      return;
    }
    // tslint:disable-next-line: max-line-length
    await axios.post(`http://localhost:5000/messages?message=${this.newMessage}&sentByUserId=${this.userId}&conversationId=${this.currentConversation.id}`);
    this.newMessage = '';
    await this.getConversation(this.currentConversation.id);
  }

  logout() {
    localStorage.clear();
    this.userId = 0;
    this.conversations = null;
    this.username = '';
  }
}

@Component({
  template: `
  <div style="height: auto;">
  <div mat-dialog-content *ngIf="users">
    <mat-form-field>
      <mat-label>Select User</mat-label>
      <mat-select [(ngModel)]="selectedUser">
        <mat-option *ngFor="let user of users" [value]="user">
          {{user.name}}
        </mat-option>
      </mat-select>
    </mat-form-field>
  </div>
  <div mat-dialog-actions>
    <button class="btn btn-primary" style="margin-right: 10px;" mat mat-button (click)="onCancelClick()">Cancel</button>
    <button class="btn btn-primary" mat mat-button (click)="onOkClick()">Ok</button>
  </div>
</div>
  `
})
export class AddConversationComponent implements OnInit {

  selectedUser: User = {
    id: 0,
    name: ''
  };
  users: User[];

  constructor(
    public dialogRef: MatDialogRef<AddConversationComponent>,
    @Inject(MAT_DIALOG_DATA) public userId: number) { }

  async ngOnInit(): Promise<void> {
    const response = await axios.get(`http://localhost:5000/users/search`);
    this.users = response.data
      .filter(u => u.id !== this.userId);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  async onOkClick(): Promise<void> {
    await axios.post(`http://localhost:5000/conversations?firstUserId=${this.userId}&secondUserId=${this.selectedUser.id}`);
    this.dialogRef.close();
  }
}
