import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import { ConversationList, Conversation } from './model/conversation.model';

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
}
